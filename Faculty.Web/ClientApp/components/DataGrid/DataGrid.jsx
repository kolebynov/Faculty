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
import ViewCreator from "../../utils/ViewCreator";

class DataGrid extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        let schema = modelSchemaProvider.getSchemaByName(this.props.modelName);
        
        return (
            <div>
                {this._renderTable(schema)}
            </div>
        );
    }

    _renderTable(schema) {
        return (
            <Table>
                <TableHeader>
                    <TableRow>
                        {schema.columns.map(column => (
                            <TableHeaderColumn key={column.name}>{column.caption || column.name}</TableHeaderColumn>
                        ))}
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {this.props.data.map(row => (
                        <TableRow key={row[schema.primaryColumnName]}>
                            {schema.columns.map(column => (
                                <TableRowColumn key={column.name}>{ViewCreator.createViewForModelValue(row[column.name], column.name, schema, row)}</TableRowColumn>
                            ))}
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        );
    }
}

DataGrid.propTypes = {
    modelName: PropTypes.string.isRequired,
    data: PropTypes.array.isRequired
};

export default DataGrid;