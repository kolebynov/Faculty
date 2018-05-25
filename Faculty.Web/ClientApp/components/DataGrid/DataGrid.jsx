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

const gridStates = {
    IDLE: 0,
    REMOVING_ROW: 1,
    LOADING_DATA: 2
};

class DataGrid extends React.PureComponent {
    constructor(props) {
        super(props);

        this.dataLoadingCount = 0;
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

    removeRow(primaryValue) {
        if (this.state.gridState === gridStates.IDLE) {
            this.setState({
                gridState: gridStates.REMOVING_ROW
            });
            let schema = modelSchemaProvider.getSchemaByName(this.props.modelName);
            new ApiService(schema.resourceName).deleteItem(primaryValue)
                .then(response => {
                    this._setNewGridState(gridStates.REMOVING_ROW, gridStates.IDLE);
                    if (response.success) {
                        const currentPage = this.state.currentPage;
                        this._loadData(this.state.data.length > 1 || currentPage === 1 ? currentPage : currentPage - 1);
                    }
                });
        }
    }

    _renderTable(schema) {
        return (
            <Table>
                <TableHeader adjustForCheckbox={false} displaySelectAll={false}>
                    <TableRow>
                        {schema.columns.map(column => (
                            <TableHeaderColumn key={column.name}>{column.caption || column.name}</TableHeaderColumn>
                        ))}
                        {this.props.rowActions.length > 0 ? <TableHeaderColumn /> : null}
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
                                {
                                    this.props.rowActions.length === 0 ? null :
                                    <TableRowColumn>
                                        {this.props.rowActions.map(rowAction => {
                                            const RowActionComponent = rowAction.component;
                                            const props = { 
                                                ...rowAction.props,
                                                key: `${row[schema.primaryColumnName]}-${rowAction.name}`,
                                                [rowAction.actionPropName]: () => this.props.onRowAction(rowAction.name, row[schema.primaryColumnName])
                                            };

                                            return <RowActionComponent {...props}/>
                                        })}
                                    </TableRowColumn>
                                }
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
        this.dataLoadingCount++;
        this.setState({
            gridState: gridStates.LOADING_DATA
        });
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
            .then(response => {
                this.dataLoadingCount--;
                if (this.dataLoadingCount === 0) {
                    this.setState({
                        data: response.data,
                        pagesCount: response.pagination.totalPages,
                        currentPage: response.pagination.currentPage,
                    })
                    this._setNewGridState(gridStates.LOADING_DATA, gridStates.IDLE);
                }
            });
    }

    _onPageChanged = (newPage) => {
        this._loadData(newPage);
    }

    _getDefaultState() {
        return {
            data: [],
            pagesCount: 1,
            currentPage: 1,
            gridState: gridStates.IDLE
        };
    }

    _getItemsPerPage() {
        return this.props.itemsPerPage;
    }

    _setNewGridState(neededOldState, newState) {
        if (this.state.gridState === neededOldState) {
            this.setState({
                gridState: newState
            });
        }
    }
}

DataGrid.propTypes = {
    modelName: PropTypes.string.isRequired,
    rootModel: PropTypes.object,
    itemsPerPage: PropTypes.number,
    rowActions: PropTypes.array,
    onRowAction: PropTypes.func 
};
DataGrid.defaultItemsPerPage = 30;
DataGrid.defaultProps = {
    rowActions: [],
    itemsPerPage: DataGrid.defaultItemsPerPage,
    onRowAction: () => {}
};

export default DataGrid;