using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text;
using System.Data;
using System.Management.Instrumentation;
using System.IO;
using System.Globalization;

namespace Campus.SchoolManagment.Web.Helper
{
    public class Excelizer
    {
        public enum DataType
        {
            String,
            Integer
        }

        private readonly string[] _headers;
        private readonly DataType[] _colunmTypes;
        private readonly DataTable _data;
        private readonly string _sheetName = "Grid1";
        private readonly SortedSet<string> _os = new SortedSet<string>();
        private string[] _sharedStrings;

        private static string ConvertIntToColumnHeader(int index)
        {
            var sb = new StringBuilder();
            while (index > 0)
            {
                if (index <= 'Z' - 'A') // index=0 -> 'A', 25 -> 'Z'
                    break;
                sb.Append(ConvertIntToColumnHeader(index / ('Z' - 'A' + 1) - 1));
                index = index % ('Z' - 'A' + 1);
            }
            sb.Append((char)('A' + index));
            return sb.ToString();
        }

        private static Row CreateRow(UInt32 index, IList<string> data)
        {
            var r = new Row { RowIndex = index };
            for (var i = 0; i < data.Count; i++)
                r.Append(new OpenXmlElement[] { CreateTextCell(ConvertIntToColumnHeader(i), index, data[i]) });

            return r;
        }

        private Row CreateRowWithSharedStrings(UInt32 index, IList<string> data)
        {
            var r = new Row { RowIndex = index };
            for (var i = 0; i < data.Count; i++)
                r.Append(new OpenXmlElement[] { CreateSharedTextCell(ConvertIntToColumnHeader(i), index, data[i]) });

            return r;
        }

        private Row CreateRowWithSharedStrings(UInt32 index, IList<string> data, IList<DataType> colunmTypes)
        {
            var r = new Row { RowIndex = index };
            for (var i = 0; i < data.Count; i++)
                if (colunmTypes != null && i < colunmTypes.Count && colunmTypes[i] == DataType.Integer)
                    r.Append(new OpenXmlElement[] { CreateNumberCell(ConvertIntToColumnHeader(i), index, data[i]) });
                else
                    r.Append(new OpenXmlElement[] { CreateSharedTextCell(ConvertIntToColumnHeader(i), index, data[i]) });

            return r;
        }

        private static Cell CreateTextCell(string header, UInt32 index, string text)
        {
            // create Cell with InlineString as a child, which has Text as a child
            return new Cell(new InlineString(new Text { Text = text }))
            {
                // Cell properties
                DataType = CellValues.InlineString,
                CellReference = header + index
            };
        }

        private Cell CreateSharedTextCell(string header, UInt32 index, string text)
        {
            for (var i = 0; i < _sharedStrings.Length; i++)
            {
                if (String.Compare(_sharedStrings[i], text, StringComparison.Ordinal) == 0)
                {
                    return new Cell(new CellValue { Text = i.ToString(CultureInfo.InvariantCulture) })
                    {
                        // Cell properties
                        DataType = CellValues.SharedString,
                        CellReference = header + index
                    };
                }
            }
            // create Cell with InlineString as a child, which has Text as a child
            throw new InstanceNotFoundException();
        }

        private static Cell CreateNumberCell(string header, UInt32 index, string numberAsString)
        {
            // create Cell with CellValue as a child, which has Text as a child
            return new Cell(new CellValue { Text = numberAsString })
            {
                // Cell properties
                CellReference = header + index
            };
        }

        private void FillSharedStringTable(IEnumerable<string> data)
        {
            foreach (var item in data)
                _os.Add(item);
        }

        private void FillSharedStringTable(IList<string> data, IList<DataType> colunmTypes)
        {
            for (var i = 0; i < data.Count; i++)
                if (colunmTypes == null || i >= colunmTypes.Count || colunmTypes[i] == DataType.String)
                    _os.Add(data[i]);
        }

        public Excelizer(string[] headers, DataTable data, string sheetName)
        {
            _headers = headers;
            _data = data;
            _sheetName = sheetName;
        }
        public Excelizer(DataTable data, string sheetName)
        {

            _data = data;
            _sheetName = sheetName;
        }

        public Excelizer(string[] headers, DataType[] colunmTypes, DataTable data, string sheetName)
        {
            _headers = headers;
            _colunmTypes = colunmTypes;
            _data = data;
            _sheetName = sheetName;
        }

        private void FillSpreadsheetDocument(SpreadsheetDocument spreadsheetDocument)
        {
            // create and fill SheetData
            spreadsheetDocument.CompressionOption = System.IO.Packaging.CompressionOption.SuperFast;

            var sheetData = new SheetData();
            var rowCount = _data.Rows.Count;

            Row row = null;
            Cell cell = null;

            // first row is the header
            sheetData.AppendChild(CreateRow(1, _headers));

            //const UInt32 iAutoFilter = 2;
            // skip next row (number 2) for the AutoFilter
            //var i = iAutoFilter + 1;
            UInt32 i = 2;

            // first of all collect all different strings in OrderedSet<string> _os
            //foreach (var dataRow in _data)
            //    if (_colunmTypes != null)
            //        FillSharedStringTable(dataRow, _colunmTypes);
            //    else
            //        FillSharedStringTable(dataRow);
            //_sharedStrings = _os.ToArray();

            for (var r = 0; r < rowCount; r++)
            {
                row = new Row();
                for (var c = 0; c < _data.Columns.Count; c++)
                {
                    cell = new Cell();

                    cell.SetAttribute(new OpenXmlAttribute("", "t", "", "inlineStr"));
                    cell.InlineString = new InlineString(new Text { Text = _data.Rows[r][c].ToString() });
                    row.Append(cell);

                }
                sheetData.AppendChild(row);
            }




            //sheetData.AppendChild(_colunmTypes != null
            //                              ? CreateRowWithSharedStrings(i++, dataRow, _colunmTypes)
            //                              : CreateRowWithSharedStrings(i++, dataRow));

            //var sst = new SharedStringTable();

            //foreach (var text in _os)
            //    sst.AppendChild(new SharedStringItem(new Text(text)));

            // add empty workbook and worksheet to the SpreadsheetDocument
            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

            // var shareStringPart = workbookPart.AddNewPart<SharedStringTablePart>();
            // shareStringPart.SharedStringTable = sst;

            //  shareStringPart.SharedStringTable.Save();

            // add sheet data to Worksheet
            worksheetPart.Worksheet = new Worksheet(sheetData);
            worksheetPart.Worksheet.Save();

            // fill workbook with the Worksheet
            spreadsheetDocument.WorkbookPart.Workbook = new Workbook(
                    new FileVersion { ApplicationName = "Microsoft Office Excel" },
                    new Sheets(
                        new Sheet
                        {
                            Name = _sheetName,
                            SheetId = (UInt32Value)1U,

                            // generate the id for sheet
                            Id = workbookPart.GetIdOfPart(worksheetPart)
                        }
                    )
                );
            spreadsheetDocument.WorkbookPart.Workbook.Save();
            spreadsheetDocument.Dispose();
        }

        private WorkbookStylesPart AddStyleSheet(SpreadsheetDocument spreadsheet)
        {
            WorkbookStylesPart stylesheet = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();

            Stylesheet workbookstylesheet = new Stylesheet();

            Font font0 = new Font();         // Default font

            Font font1 = new Font();          // Bold font
            Bold bold = new Bold();
            font1.Append(bold);

            Fonts fonts = new Fonts();      // <APENDING Fonts>
            fonts.Append(font0);
            fonts.Append(font1);

            // <Fills>
            Fill fill0 = new Fill();        // Default fill

            Fills fills = new Fills();      // <APENDING Fills>
            fills.Append(fill0);

            // <Borders>
            Border border0 = new Border();     // Defualt border

            Borders borders = new Borders();    // <APENDING Borders>
            borders.Append(border0);

            // <CellFormats>
            CellFormat cellformat0 = new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 }; // Default style : Mandatory | Style ID =0

            CellFormat cellformat1 = new CellFormat() { FontId = 1 };  // Style with Bold text ; Style ID = 1


            // <APENDING CellFormats>
            CellFormats cellformats = new CellFormats();
            cellformats.Append(cellformat0);
            cellformats.Append(cellformat1);


            // Append FONTS, FILLS , BORDERS & CellFormats to stylesheet <Preserve the ORDER>
            workbookstylesheet.Append(fonts);
            workbookstylesheet.Append(fills);
            workbookstylesheet.Append(borders);
            workbookstylesheet.Append(cellformats);

            // Finalize
            stylesheet.Stylesheet = workbookstylesheet;
            stylesheet.Stylesheet.Save();

            return stylesheet;
        }

        private Stylesheet AddStyleSheet2()
        {

            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - default
                    new FontSize() { Val = 12 }
                ),
                new Font( // Index 1 - header
                    new FontSize() { Val = 13 },
                    new Bold(),
                    new Color() { Rgb = "FFFFFF" }

                ));

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "2ca8ff" } })
                    { PatternType = PatternValues.Solid }) // Index 2 - header
                );

            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Rgb = new HexBinaryValue() { Value = "2ca8ff" } }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Rgb = new HexBinaryValue() { Value = "2ca8ff" } }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Rgb = new HexBinaryValue() { Value = "2ca8ff" } }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Rgb = new HexBinaryValue() { Value = "2ca8ff" } }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                );

            CellFormats cellFormats = new CellFormats(
                    new CellFormat(), // default
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 0, ApplyFill = true }, // header
                     new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyProtection = true } // body protect
                );



            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }

        private void FillSpreadsheetDocumentSAX(SpreadsheetDocument spreadsheetDocument)
        {

            //spreadsheetDocument.CompressionOption = System.IO.Packaging.CompressionOption.SuperFast;

            Int64 rowCount = _data.Rows.Count;
            DateTime date;

            List<OpenXmlAttribute> oxa;

            OpenXmlWriter oxw;


            WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();


            WorksheetPart wsp = spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();

            //AddStyleSheet(spreadsheetDocument);
            WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();

            stylePart.Stylesheet = AddStyleSheet2();
            stylePart.Stylesheet.Save();


            oxw = OpenXmlWriter.Create(wsp);

            oxw.WriteStartElement(new Worksheet());



            oxw.WriteStartElement(new Columns());

            oxa = new List<OpenXmlAttribute>();
            // min and max are required attributes
            // This means from columns 2 to 4, both inclusive
            oxa.Add(new OpenXmlAttribute("min", null, "2"));
            oxa.Add(new OpenXmlAttribute("max", null, "100"));
            oxa.Add(new OpenXmlAttribute("width", null, "20"));

            oxw.WriteStartElement(new Column(), oxa);

            oxw.WriteEndElement();

            oxa = new List<OpenXmlAttribute>();
            // min and max are required attributes
            // This means from columns 2 to 4, both inclusive
            oxa.Add(new OpenXmlAttribute("min", null, "1"));
            oxa.Add(new OpenXmlAttribute("max", null, "1"));
            oxa.Add(new OpenXmlAttribute("Hidden", null, "true"));

            oxw.WriteStartElement(new Column(), oxa);

            oxw.WriteEndElement();

            oxw.WriteEndElement();

            oxw.WriteStartElement(new SheetData());

            //for header
            oxa = new List<OpenXmlAttribute>();
            oxw.WriteStartElement(new Row(), oxa);

            for (int h = 0; h < _data.Columns.Count; h++)
            {
                oxa = new List<OpenXmlAttribute>();
                oxa.Add(new OpenXmlAttribute("t", null, "str"));
                oxa.Add(new OpenXmlAttribute("s", null, "2"));

                oxw.WriteStartElement(new Cell(), oxa);

                oxw.WriteElement(new CellValue(_data.Columns[h].ColumnName));


                // this is for Cell
                oxw.WriteEndElement();
            }
            oxw.WriteEndElement();



            for (int i = 0; i < rowCount; i++)
            {
                oxa = new List<OpenXmlAttribute>();
                // this is the row index
                // oxa.Add(new OpenXmlAttribute("r", null, i.ToString()));

                oxw.WriteStartElement(new Row(), oxa);

                for (int j = 0; j < _data.Columns.Count; j++)
                {
                    oxa = new List<OpenXmlAttribute>();

                    if (j == 0)
                    {
                        oxa.Add(new OpenXmlAttribute("t", null, "n"));
                        oxa.Add(new OpenXmlAttribute("s", null, "1"));
                        oxw.WriteStartElement(new Cell(), oxa);
                        oxw.WriteElement(new CellValue(_data.Rows[i][j].ToString()));
                    }

                    else if (_data.Columns[j].DataType.Name.Equals("String") || _data.Columns[j].DataType.Name.Equals("Guid"))
                    {
                        oxa.Add(new OpenXmlAttribute("t", null, "str"));
                        oxa.Add(new OpenXmlAttribute("s", null, "1"));
                        oxw.WriteStartElement(new Cell(), oxa);
                        oxw.WriteElement(new CellValue(_data.Rows[i][j].ToString()));
                    }
                    else if (_data.Columns[j].DataType.Name.Equals("DateTime"))
                    {

                        oxa.Add(new OpenXmlAttribute("t", null, "d"));
                        oxa.Add(new OpenXmlAttribute("s", null, "1"));
                        oxw.WriteStartElement(new Cell(), oxa);

                        oxw.WriteElement(new CellValue(_data.Rows[i][j].ToString()));
                    }
                    else if (_data.Columns[j].DataType.Name.Equals("Date"))
                    {

                        oxa.Add(new OpenXmlAttribute("t", null, "d"));
                        oxa.Add(new OpenXmlAttribute("s", null, "1"));
                        oxw.WriteStartElement(new Cell(), oxa);
                        date = (DateTime)_data.Rows[i][j];
                        oxw.WriteElement(new CellValue(date.ToString()));
                    }
                    else
                    {
                        oxa.Add(new OpenXmlAttribute("t", null, "n"));
                        oxa.Add(new OpenXmlAttribute("s", null, "1"));
                        oxw.WriteStartElement(new Cell(), oxa);
                        oxw.WriteElement(new CellValue(_data.Rows[i][j].ToString()));
                    }

                    // this is for Cell
                    oxw.WriteEndElement();
                }

                // this is for Row
                oxw.WriteEndElement();
            }

            // this is for SheetData
            oxw.WriteEndElement();
            // this is for Worksheet
            oxw.WriteEndElement();
            oxw.Close();

            oxw = OpenXmlWriter.Create(spreadsheetDocument.WorkbookPart);
            oxw.WriteStartElement(new Workbook());
            oxw.WriteStartElement(new Sheets());
            oxw.WriteElement(new Sheet()
            {
                Name = _sheetName,
                SheetId = 1,
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(wsp)
            });
            // this is for Sheets
            oxw.WriteEndElement();
            // this is for Workbook
            oxw.WriteEndElement();
            oxw.Close();

            spreadsheetDocument.Dispose();

        }

        public void CreateXlsxAndFillData(Stream stream)
        {
            // Create workbook document
            using (var spreadsheetDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                //FillSpreadsheetDocument(spreadsheetDocument);
                FillSpreadsheetDocumentSAX(spreadsheetDocument);
            }
        }

    }
}