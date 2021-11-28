using System;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace OpenXmlDocs.Extensions
{
    public static class Layout
    {
        public static Table Base(Action<Table> generate)
        {
            var table = new Table(
                new TableProperties(
                    new TableStyle() { Val = "TableGrid" },
                    new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct }
                ),
                new TableGrid(new GridColumn(), new GridColumn(), new GridColumn())
            );

            generate(table);

            return table;
        }

        public static TableRow Row(Action<TableRow> generate)
        {
            var row = new TableRow();
            generate(row);

            return row;
        }

        public static TableCell Content(OpenXmlElement element, Func<TableCellProperties>? format = null)
        {
            var content = new TableCell();

            content.Append(new TableCellProperties(
                Builder.GenerateTableCellMargin("80")
            ));

            if (format is not null)
                content.Append(format());

            content.AppendChild(element);

            return content;
        }

        public static TableCell Content(Func<TableCellProperties>? format = null, params OpenXmlElement[] elements)
        {
            var content = new TableCell();

            content.Append(new TableCellProperties(
                Builder.GenerateTableCellMargin("80")
            ));

            if (format is not null)
                content.Append(format());

            content.Append(elements);

            return content;
        }
    }
}