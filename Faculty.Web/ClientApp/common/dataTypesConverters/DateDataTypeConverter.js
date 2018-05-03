import BaseDataTypeConverter from "./BaseDataTypeConverter";

class DateDataTypeConverter extends BaseDataTypeConverter {
    toString(value) {
        return value.toString();
    }

    fromString(str) {
        return Date.parse(str);
    }
}

export default DateDataTypeConverter;