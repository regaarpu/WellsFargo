using ExcelDataReader;
using OMS.Entity;
using System.Data;
using System.Text;

namespace OMS
{
    public partial class OMS : Form
    {
        List<string> workBookList = null;
        public OMS()
        {
            InitializeComponent();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            workBookList = new List<string>();
            workBookList.Add("portfolios.csv");
            workBookList.Add("securities.csv");
            workBookList.Add("transactions.csv");
        }

        private void btnGenerateOutput_Click(object sender, EventArgs e)
        {
            DataSet dsSheet = null;
            try
            {
                if (string.IsNullOrEmpty(txtExcelFilePath.Text.Trim()))
                {
                    MessageBox.Show("Please provide folder path to process..!", "OMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    List<Portfolios> portfolioList = new List<Portfolios>();
                    List<Securities> securityList = new List<Securities>();
                    List<Transactions> transactionList = new List<Transactions>();

                    foreach (var workbook in workBookList)
                    {
                        using var stream = new FileStream(txtExcelFilePath.Text + @"\" + workbook
                                                        , FileMode.Open
                                                        , FileAccess.Read
                                                        , FileShare.ReadWrite);

                        using IExcelDataReader reader = ExcelReaderFactory.CreateCsvReader(stream);

                        dsSheet = reader.AsDataSet();

                        var sheetDataTable = dsSheet.Tables[0];

                        int excelReadStartColumn = 1;
                        int excelReadEndColumn = sheetDataTable.Columns.Count;

                        int excelReadStartRow = 2;
                        int excelReadEndRow = sheetDataTable.Rows.Count;

                        int colCount = excelReadEndColumn - excelReadStartColumn + 1;

                        int rCnt = 0;
                        int cCnt = 0;
                        int rw = sheetDataTable.Rows.Count;
                        int cl = colCount;
                        string sheetRowValue = string.Empty;

                        for (rCnt = excelReadStartRow - 1; rCnt < rw; rCnt++)
                        {
                            for (cCnt = 0; cCnt < cl; cCnt++)
                            {
                                sheetRowValue = string.Empty;

                                int curPortfolioId = 0;
                                string curPortfolioCode = string.Empty;
                                switch (workbook)
                                {
                                    case "portfolios.csv":
                                        {
                                            Portfolios portfolio = new Portfolios();
                                            curPortfolioId = Convert.ToInt32(sheetDataTable.Rows[rCnt][cCnt]);
                                            cCnt++; curPortfolioCode = Convert.ToString(sheetDataTable.Rows[rCnt][cCnt]);
                                            portfolio.PortfolioId = curPortfolioId;
                                            portfolio.PortfolioCode = curPortfolioCode;
                                            portfolioList.Add(portfolio);
                                            break;
                                        }
                                    case "securities.csv":
                                        {
                                            Securities securities = new Securities();
                                            securities.SecurityId = Convert.ToInt32(sheetDataTable.Rows[rCnt][cCnt]);
                                            cCnt++; securities.ISIN = Convert.ToString(sheetDataTable.Rows[rCnt][cCnt]);
                                            cCnt++; securities.Ticker = Convert.ToString(sheetDataTable.Rows[rCnt][cCnt]);
                                            cCnt++; securities.Cusip = Convert.ToString(sheetDataTable.Rows[rCnt][cCnt]);
                                            securityList.Add(securities);
                                            break;
                                        }
                                    case "transactions.csv":
                                        {
                                            Transactions transactions = new Transactions();
                                            transactions.SecurityId = Convert.ToInt32(sheetDataTable.Rows[rCnt][cCnt]);
                                            cCnt++; transactions.PortfolioId = Convert.ToInt32(sheetDataTable.Rows[rCnt][cCnt]);
                                            cCnt++; transactions.Nominal = Convert.ToDecimal(sheetDataTable.Rows[rCnt][cCnt]);
                                            cCnt++; transactions.OMS = Convert.ToString(sheetDataTable.Rows[rCnt][cCnt]);
                                            cCnt++; transactions.TransactionType = Convert.ToString(sheetDataTable.Rows[rCnt][cCnt]);
                                            transactionList.Add(transactions);
                                            break;
                                        }
                                }
                            }
                        }
                    }

                    var result = from t1 in transactionList
                                 join t2 in portfolioList on t1.PortfolioId equals t2.PortfolioId
                                 select new { t2.PortfolioCode, t1.OMS, t1.TransactionType, t1.Nominal, t1.SecurityId } into intermediate
                                 join t3 in securityList on intermediate.SecurityId equals t3.SecurityId
                                 select new { intermediate.PortfolioCode, intermediate.OMS, intermediate.TransactionType, intermediate.Nominal, t3.ISIN, t3.Cusip, t3.Ticker };


                    var aaaList = result.Where(a => a.OMS.Equals("AAA")).Select(x => new { x.ISIN, x.PortfolioCode, x.Nominal, x.TransactionType }).ToList();

                    var csv = new StringBuilder();
                    csv.AppendLine("ISIN,PortfolioCode,Nominal,TransactionType");
                    foreach (var aaa in aaaList)
                    {
                        var ISIN = aaa.ISIN.ToString();
                        var PortfolioCode = aaa.PortfolioCode.ToString();
                        var Nominal = aaa.Nominal.ToString();
                        var TransactionType = aaa.TransactionType.ToString();
                        var newLine = $"{ISIN},{PortfolioCode},{Nominal},{TransactionType}";
                        csv.AppendLine(newLine);
                    }
                    File.WriteAllText(txtExcelFilePath.Text + @"\" + "aaa.csv", csv.ToString());

                    var bbbList = result.Where(a => a.OMS.Equals("BBB")).Select(x => new { x.Cusip, x.PortfolioCode, x.Nominal, x.TransactionType }).ToList();

                    csv = new StringBuilder();
                    csv.AppendLine("Cusip,PortfolioCode,Nominal,TransactionType");
                    foreach (var bbb in bbbList)
                    {
                        var Cusip = bbb.Cusip.ToString();
                        var PortfolioCode = bbb.PortfolioCode.ToString();
                        var Nominal = bbb.Nominal.ToString();
                        var TransactionType = bbb.TransactionType.ToString();
                        var newLine = $"{Cusip},{PortfolioCode},{Nominal},{TransactionType}";
                        csv.AppendLine(newLine);
                    }
                    File.WriteAllText(txtExcelFilePath.Text + @"\" + "bbb.csv", csv.ToString());

                    var cccList = result.Where(a => a.OMS.Equals("CCC")).Select(x => new { x.PortfolioCode, x.Ticker, x.Nominal, x.TransactionType }).ToList();
                    csv = new StringBuilder();
                    csv.AppendLine("PortfolioCode,Ticker,Nominal,TransactionType");
                    foreach (var ccc in cccList)
                    {
                        var PortfolioCode = ccc.PortfolioCode.ToString();
                        var Ticker = ccc.Ticker.ToString();
                        var Nominal = ccc.Nominal.ToString();
                        var TransactionType = ccc.TransactionType.ToString();
                        var newLine = $"{PortfolioCode},{Ticker},{Nominal},{TransactionType}";
                        csv.AppendLine(newLine);
                    }
                    File.WriteAllText(txtExcelFilePath.Text + @"\" + "ccc.csv", csv.ToString());


                    MessageBox.Show("OMS Output Generated successfully in " + txtExcelFilePath.Text);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}