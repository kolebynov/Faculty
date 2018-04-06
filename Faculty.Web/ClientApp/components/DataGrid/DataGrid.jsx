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

class DataGrid extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        let schema = modelSchemaProvider.getSchemaByName(this.props.modelName);
        
        return (
            <div>
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
                                    <TableRowColumn key={column.name}>{row[column.name]}</TableRowColumn>
                                ))}
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
                
            </div>
        );
    }
}

DataGrid.propTypes = {
    modelName: PropTypes.string.isRequired,
    data: PropTypes.array.isRequired,
    pagesCount: PropTypes.number
};
DataGrid.defaultProps = {
    pagesCount: 1
};

export default DataGrid;