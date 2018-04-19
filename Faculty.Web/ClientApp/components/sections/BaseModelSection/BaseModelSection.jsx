import React from "react";
import PropTypes from "prop-types";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import DataGrid from "../../DataGrid/DataGrid.jsx";
import ApiService from "../../../services/ApiService";
import Pagination from "../../Pagination/Pagination.jsx";

class BaseModelSection extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: [],
            pagesCount: 1,
            currentPage: 1
        };

        this._onPageChanged = this._onPageChanged.bind(this);
    }

    componentDidMount() {
        this._loadData(this.state.currentPage);
    }

    render() {
        return (
            <div>
                <DataGrid modelName={this.props.modelName} data={this.state.data} />
                <Pagination pagesCount={this.state.pagesCount} onPageChange={this._onPageChanged} initialPage={this.state.currentPage} />
            </div>
        );
    }

    _loadData(page) {
        let apiService = new ApiService(modelSchemaProvider.getSchemaByName(this.props.modelName).resourceName);
        apiService.getItems(null, {page: page, rowsCount: BaseModelSection.defaultItemsPerPage})
            .then(response => this.setState({
                data: response.data,
                pagesCount: response.pagination.totalPages,
                currentPage: response.pagination.currentPage
            }));
    }

    _onPageChanged(newPage) {
        this._loadData(newPage);
    }
}

BaseModelSection.propTypes = {
    modelName: PropTypes.string
};
BaseModelSection.defaultItemsPerPage = 30;

export default BaseModelSection;