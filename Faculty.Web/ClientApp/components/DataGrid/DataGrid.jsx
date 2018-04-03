import React from "react";
import PropTypes from "prop-types";
import modelSchemaProvider from "../../schemas/ModelSchemaProvider";
import ApiService from "../../services/ApiService";
import { 
    Table,
    TableBody,
    TableHeader,
    TableHeaderColumn,
    TableRow,
    TableRowColumn
} from "material-ui/Table";

class DataGrid extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: []
        };
    }

    componentDidMount() {
        this._loadData();
    }

    render() {
        let schema = modelSchemaProvider.getSchemaByName(this.props.modelName);
        return (
            <Table>
                <TableHeader>
                    <TableRow>
                        {schema.columns.map(column => (
                            <TableHeaderColumn key={column.name}>{column.name}</TableHeaderColumn>
                        ))}
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {this.state.data.map(row => (
                        <TableRow key={row[schema.primaryColumnName]}>
                            {schema.columns.map(column => (
                                <TableRowColumn key={column.name}>{row[column.name]}</TableRowColumn>
                            ))}
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        );
    }

    _loadData() {
        let apiService = new ApiService(modelSchemaProvider.getSchemaByName(this.props.modelName).resourceName);
        apiService.getItems()
            .then(response => this.setState({
                data: response.data
            }))
    }
}

DataGrid.propTypes = {
    modelName: PropTypes.string
}

export default DataGrid;