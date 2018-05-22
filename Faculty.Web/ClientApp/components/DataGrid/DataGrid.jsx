import React from "react";
import PropTypes from "prop-types";
import modelSchemaProvider from "../../schemas/ModelSchemaProvider";
import { 
    Table,
    TableBody,
    TableHeader,
    TableHeaderColumn,
    TableRow,
    TableRowColumn
} from "material-ui/Table";
import Pagination from "../Pagination/Pagination.jsx";
import ApiService from "../../services/ApiService";
import ModelValueView from "../ModelValueView/ModelValueView.jsx";

class DataGrid extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = this._getDefaultState();
    }
    
    componentDidMount() {
        this._loadCurrentPage();
    }

    componentWillReceiveProps(newProps) {
        if (this.props.modelName !== newProps.modelName) {
            this.setState(this._getDefaultState(), this._loadCurrentPage);
        }
    }

    render() {
        let schema = modelSchemaProvider.getSchemaByName(this.props.modelName);
        
        return (
            <div>
                {this._renderTable(schema)}
                {this._renderPagination()}
            </div>
        );
    }

    _renderTable(schema) {
        return (
            <Table>
                <TableHeader adjustForCheckbox={false} displaySelectAll={false}>
                    <TableRow>
                        {schema.columns.map(column => (
                            <TableHeaderColumn key={column.name}>{column.caption || column.name}</TableHeaderColumn>
                        ))}
                    </TableRow>
                </TableHeader>
                <TableBody displayRowCheckbox={false}>
                    {this.state.data.map(row => (
                        <TableRow key={row[schema.primaryColumnName]}>
                            {schema.columns.map(column => (
                                <TableRowColumn key={column.name}>
                                    <ModelValueView columnName={column.name} schema={schema} model={row}/>
                                </TableRowColumn>
                            ))}
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        );
    }

    _renderPagination() {
        return <Pagination pagesCount={this.state.pagesCount} onPageChange={this._onPageChanged} 
            initialPage={this.state.currentPage} />;
    }

    _loadCurrentPage = () => this._loadData(this.state.currentPage)

    _loadData(page) {
        let modelName = this.props.modelName;
        let id = null;
        let linkedResouce = null;
        if (this.props.rootModel) {
            modelName = this.props.rootModel.name;
            id = this.props.rootModel.primaryValue;
            linkedResouce = modelSchemaProvider.getSchemaByName(this.props.modelName).resourceName;
        }
        let apiService = new ApiService(modelSchemaProvider.getSchemaByName(modelName).resourceName);
        apiService.getItems(id, {page: page, rowsCount: this._getItemsPerPage()}, linkedResouce)
            .then(response => this.setState({
                data: response.data,
                pagesCount: response.pagination.totalPages,
                currentPage: response.pagination.currentPage
            }));
    }

    _onPageChanged = (newPage) => {
        this._loadData(newPage);
    }

    _getDefaultState() {
        return {
            data: [],
            pagesCount: 1,
            currentPage: 1
        };
    }

    _getItemsPerPage() {
        return this.props.itemsPerPage || DataGrid.defaultItemsPerPage;
    }
}

DataGrid.propTypes = {
    modelName: PropTypes.string.isRequired,
    rootModel: PropTypes.object,
    itemsPerPage: PropTypes.number
};
DataGrid.defaultItemsPerPage = 30;

export default DataGrid;