// {C0FCA290-5B99-4F78-924E-F47C8DB67752} <-- upgrade code do not change 
//#define USBBOOT
//using Microsoft.VisualBasic;
using Renci.SshNet;
//using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Data;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Runtime.ExceptionServices;
using websoku86v6;

namespace websoku86v6
{
    public partial class Form1 : Form
    {
        readonly static string myName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
#if USBBOOT
        readonly static string workDir = "..\\html\\";
        readonly static string iniFile =  myName + ".config";
#else
        readonly static string workDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
             "\\" + myName + "\\";
        readonly static string iniFile = workDir + myName + ".config";
#endif
        public static string ServerName = "daisy", eventNoStr = "", htmlPath = "",
                indexFile = "", prgResult = "", rankingFile = "", scoreFile = "",
                secKeyFile = "";
        string hostName = "", port = "22", userName = "";
        public Form1()
        {
            InitializeComponent();
            this.Width = 700;
            this.Height = 600;


            Misc.ReadConfigFile(iniFile, ref ServerName, ref eventNoStr,
                ref htmlPath, ref indexFile, ref prgResult, ref rankingFile,
                ref scoreFile, ref hostName, ref port, ref userName, ref secKeyFile);
            txtBoxServerName.Text = ServerName;
            txtBoxEventNo.Text = eventNoStr.Trim();
            txtBoxIndexFile.Text = indexFile;
            txtBoxPrgResult.Text = prgResult;
            txtBoxScoreFile.Text = scoreFile;
            txtBoxRanking.Text = rankingFile;
            txtBoxHtmlPath.Text = htmlPath;
            txtBoxHostName.Text = hostName;
            if (port == "")
            {
                port = "22";
            }
            txtBoxPort.Text = port;
            txtBoxUserName.Text = userName;
            txtBoxKeyFile.Text = secKeyFile;
            InitTimer();
        }
        public bool MustSendCSS()
        {
            return this.chkBoxInitSend.Checked;
        }
        public static System.Windows.Forms.Timer timer;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = workDir + txtBoxIndexFile.Text;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
        public static EventHandler ev1;
        private void InitTimer()
        {
            timer = new System.Windows.Forms.Timer();
            ev1 = new EventHandler(CreateRun);
            timer.Tick += ev1;
            timer.Enabled = false;

        }
        private void CreateRun(object sender, EventArgs ev)
        {
            Cursor.Current = Cursors.WaitCursor;
            Misc.WriteConfigFile(iniFile, txtBoxServerName.Text, txtBoxEventNo.Text,
                txtBoxHtmlPath.Text, txtBoxIndexFile.Text, txtBoxPrgResult.Text, txtBoxRanking.Text,
                txtBoxScoreFile.Text, txtBoxHostName.Text, txtBoxPort.Text,
                txtBoxUserName.Text, txtBoxKeyFile.Text);
            Html.CreateHTML(
                txtBoxServerName.Text,
                int.Parse(txtBoxEventNo.Text),
                workDir,
                txtBoxIndexFile.Text,
                txtBoxRanking.Text,
                txtBoxPrgResult.Text,
                txtBoxScoreFile.Text,
                txtBoxHtmlPath.Text,
                txtBoxHostName.Text,
                Convert.ToInt32(txtBoxPort.Text),
                txtBoxUserName.Text,
                txtBoxKeyFile.Text,
                txtBoxYouTube.Text,
                chkBoxInitSend.Checked);
            Cursor.Current = Cursors.Default;

        }
        private void btnAutoRun_Click(object sender, EventArgs e)
        {
            int interval;
            try
            {
                if (txtBoxInterval.Text == "") interval = 300000;
                else interval = Convert.ToInt32(txtBoxInterval.Text) * 60000;
            }
            catch
            {
                interval = 300000;
            }
            if (interval == 0) interval = 300000;
            if (!timer.Enabled)
            {
                timer.Enabled = true;
                timer.Interval = interval;
                btnAutoRun.Text = "停止";
            }
            else
            {
                timer.Enabled = false;
                btnAutoRun.Text = "開始";
            }
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Misc.WriteConfigFile(iniFile, txtBoxServerName.Text, txtBoxEventNo.Text,
                txtBoxHtmlPath.Text, txtBoxIndexFile.Text, txtBoxPrgResult.Text, txtBoxRanking.Text,
                txtBoxScoreFile.Text, txtBoxHostName.Text, txtBoxPort.Text,
                txtBoxUserName.Text, txtBoxKeyFile.Text);
            this.Close();
        }
        private void btnConfirmServer_Click(object sender, EventArgs e)
        {
            if (MDBInterface.ServerAccessOK(txtBoxServerName.Text))
            {
                MessageBox.Show(" Server Connection OK. Go ahead.");
            }
            else
            {
                MessageBox.Show(" Error: Server 接続に失敗しました。Serverの名前を確認してください。", "!!ERROR!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnKeyFile_Click(object sender, EventArgs e)
        {
            try
            {
                secKeyFile = Misc.GetFilePath("秘密鍵Fileを選択してください",
                    Path.GetDirectoryName(txtBoxKeyFile.Text), "秘密鍵ファイル(*.pem)|*.pem|すべてのファイル|*.*");
            }
            catch
            {
                secKeyFile = Misc.GetFilePath("秘密鍵Fileを選択してください",
                    "C:\\", "秘密鍵ファイル(*.pem)|*.pem");
            }
            txtBoxKeyFile.Text = secKeyFile;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            CreateRun(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + "    v1.1";
        }
    }
    public class Result
    {
        public int uid;
        public int kumi;
        public int goalTime;
        public string[] lapTime = new string[61];
        public int swimmerID;
        public int[] rswimmer = new int[4];
        public int laneNo;
        public int reasonCode;
        public int rank;
        public string newRecord;
    }

    public static class Html
    {
        static string thisYouTubeURL = "";
        //        static string extraMessageURL = "https://result.swim.or.jp/tournament/list?member_group_code=25&official_group_code=25";
        //        static string extraMessage = "県内の過去のレースの結果はこちらです。";
        public static void CreateHTML(
            //string mdbFile,    // Seiko Result System database.
            string serverName,
            int eventNo,     //  V6 support
            string workDir,    // local PC work directory
            string indexFile,  //indexFile   is something like ossSpring2024.Html
            string rankingFile,//rankingFile is something like ossSpring2024r.Html
            string kanproFile, //kanproFile  is something like ossSpring2024p.Html
            string scoreFile,  //scoreFile   is something like ossSpring2024s.Html
            string htmlDir,    //htmlDir     is something like rFlash
            string hostName,
            int port,
            string userName,
            string keyFile,
            string youTubeURL,
            bool mustSendCSS)
        {
            string indexFilePath;
            string rankingFilePath;
            string kanproFilePath;
            string teamScoreFilePath;
            string distDir = htmlDir + "/";
            MDBInterface mdb2Html;
            thisYouTubeURL = youTubeURL;
            if (!Directory.Exists(workDir))
            {
                MessageBox.Show("作業フォルダー(" + workDir + ")が見つかりません。");
                return;
            }
            mdb2Html = new MDBInterface(serverName, eventNo);

            ///Call init_machin_specific_variables
            indexFilePath = distDir + indexFile;
            rankingFilePath = distDir + rankingFile;
            kanproFilePath = distDir + kanproFile; //from txtBox
            teamScoreFilePath = distDir + scoreFile;
            if (indexFile != string.Empty)
            {
                string srcFile = workDir + "\\" + indexFile;
                CreateIndexHTML(mdb2Html, srcFile, rankingFile, kanproFile);
                if (keyFile != "")
                    Misc.SendFile(srcFile, indexFilePath, hostName, port, userName, keyFile);
            }
            if (rankingFile != string.Empty)
            {
                string srcFile = workDir + "\\" + rankingFile;
                CreateRankingFile(eventNo,srcFile,indexFile, kanproFile);
                if (keyFile != "")
                    Misc.SendFile(srcFile, rankingFilePath, hostName, port, userName, keyFile);
            }

            if (kanproFile != string.Empty)
            {
                string srcFile = workDir + "\\" + kanproFile;
                CreateHTMLProgramFormat(mdb2Html, srcFile, indexFile, rankingFile);
                if (keyFile != "")
                    Misc.SendFile(srcFile, kanproFilePath, hostName, port, userName, keyFile);
            }
            if (teamScoreFilePath != string.Empty)
            {

                //read_score_rule();
                //gen_team_score_html(teamScoreFilePath);

            }
            if (mustSendCSS)
            {
                Misc.SendFile(workDir + "\\cman.js", distDir + "cman.js", hostName, port, userName, keyFile);
                Misc.SendFile(workDir + "\\css\\cman.css", distDir + "css/cman.css", hostName, port, userName, keyFile);
                Misc.SendFile(workDir + "\\css\\swim.css", distDir + "css/swim.css", hostName, port, userName, keyFile);
            }

        }


        static void PrintShumoku(StreamWriter sw, int prgNo, string className, string gender, string distance, string style, string phase, string gameRecord)
        {
            sw.WriteLine("<hr id=\"PRGH" + prgNo + "\">");
            sw.WriteLine("<table width=\"95%\">");
            sw.WriteLine("  <tr>");
            sw.WriteLine("    <td>  No. " + prgNo + "</td> <td>" + gender + "</td><td>" +
                         className + "</td>" +
                         "<td align=\"right\">" + distance + "</td>" +
                         "<td align=\"left\">" + style + "</td>" +
                         "<td align=\"right\">" + phase + "&nbsp;&nbsp;");

            sw.WriteLine("大会記録:" + gameRecord);
            sw.WriteLine("</td></tr></table>");
            sw.WriteLine("<hr>");
        }


        static void PrintShumoku(MDBInterface mdb, StreamWriter sw, int prgNo)
        {
            sw.WriteLine("<hr id=\"PRGH" + prgNo + "\">");
            sw.WriteLine("<table width=\"95%\">");
            sw.WriteLine("  <tr>");
            sw.WriteLine("    <td>  No. " + prgNo + "</td> <td>" +
                         mdb.GetGenderFromPrgNo(prgNo) + "</td><td>" +
                         mdb.GetClassFromPrgNo(prgNo) + "</td>" +
                         "<td align=\"right\">" + mdb.GetDistanceFromPrgNo(prgNo) + "</td>" +
                         "<td align=\"left\">" + mdb.GetStyleFromPrgNo(prgNo) + "</td>" +
                         "<td align=\"right\">" + mdb.GetPhaseFromPrgNo(prgNo) + "&nbsp;&nbsp;");

            if (mdb.GameRecordAvailable)
            {
                sw.WriteLine("大会記録:" + Misc.TimeIntToStr(MDBInterface.GetGameRecord(prgNo)));
            }
            sw.WriteLine("</td></tr></table>");
            sw.WriteLine("<hr>");
        }

          //           touchBoard   lapCode  lapInterval
          //長水路両側   1           2           50m毎
          //長水路片側   2           4          100m
          //短水路両側   3           1           25m
          //短水路片側   4           2           50m    


        static string GetLap(int ith, int lapCode)
        {
            int intLap
                = 25 * lapCode *ith;

            if (intLap<100)
                return   Convert.ToString(intLap) + "m";
            if (intLap < 1000)
                return   Convert.ToString(intLap) + "m";
            return Convert.ToString(intLap) + "m";

        }
        static void CreateRankingFile(int eventNo, string srcFile, string indexFile, string prgFile)
        {
            const string magicWord = "\\SQLEXPRESS;User ID=Sw;Password=;Database=Sw;TrustServerCertificate=True;Encrypt=True";
            string connectionString = $" Server={MDBInterface.SERVERName}{magicWord}";

            string myQuery = " exec GetRanking @eventNo";
   
            bool first = true;
            int classNo=0;
            int prgNo=0;
            int classNoSave = 0;
            int prgNoSave = 0;
            using (StreamWriter sw = new StreamWriter(srcFile, false, System.Text.Encoding.GetEncoding("shift_jis"))) {

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand comm = new SqlCommand(myQuery, conn);
                comm.Parameters.Add("@eventNo", SqlDbType.Int).Value = eventNo;
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    bool inTable = false;
                    while(dr.Read())
                    {
                        string swimmerName;
                        swimmerName = (string)dr["第1泳者"];

                        if (first) {
                            first=false;
                            PrintHTMLHead( sw,MDBInterface.GetEventName(), MDBInterface.GetEventDate(),MDBInterface.GetEventVenue(), 2);
                        }
                        if (MDBInterface.ClassExist)
                        classNo = Convert.ToInt32(dr["クラス番号"]);
                        prgNo = Convert.ToInt32(dr["PRGNO"]);
                        if ((classNo != classNoSave )||(prgNo !=prgNoSave)) {
                            if (inTable)
                                sw.WriteLine("</table>");
                            if ((string)dr["第2泳者"] != "")
                                swimmerName = swimmerName + "<br>"
                                    + (string)dr["第2泳者"] + "<br>"
                                    + (string)dr["第3泳者"] + "<br>"
                                    + (string)dr["第4泳者"]; 


                            prgNoSave = prgNo;
                            classNoSave = classNo;
                            //static void PrintShumoku(StreamWriter sw, int prgNo, string className, string gender, string distance, string style, string phase, string gameRecord)
                            string className="";
                            if (MDBInterface.ClassExist) className = dr["クラス名称"] == DBNull.Value
                                        ? ""
                                        : (string)dr["クラス名称"];
                            PrintShumoku(sw, prgNo, className, (string)dr["性別"],
                                  (string)dr["距離"], (string)dr["種目"], (string)dr["予決"], 
                                  Misc.TimeIntToStr(MDBInterface.GetGameRecord(prgNo)));
                            sw.WriteLine("<div class=\"ahtag\" align=\"right\"> <a href=\"" + prgFile + "#PRGH" + prgNo + "\">レーン順の結果</a>&nbsp;");
                            sw.WriteLine("<a href=\"" + indexFile + "\">種目選択に戻る</a></div>");
                            sw.WriteLine("<br><br>");
                            sw.WriteLine("<table border=\"0\" width=\"100%\">");
                            inTable = true;
                            sw.WriteLine(@"<tr><th align=""left"" width=""8%"">順位</th>
                                <th align=""left"" width=""26%"">氏名</th>
                                <th align=""left"" width=""37%"">所属</th>
                                <th align=""left"" width=""17%"">タイム</th>
                                <th align=""left"" width=""12%"">新記録</th></tr>");

                        }
                        if (Convert.ToInt32(dr["事由表示"])==0) {

                            sw.WriteLine("<tr><td align=\"right\" valign=\"top\" style=\"padding-right: 10px\">" + Convert.ToInt32(dr["順位"]) + "</td>");
                        }
                        else {

                            sw.WriteLine("<tr><td align=\"right\" valign=\"top\" style=\"padding-right: 10px\">  </td>");
                        }


                        sw.WriteLine("<td valign=\"top\">" + swimmerName + "</td>");
                        sw.WriteLine("<td valign=\"top\">" + (string)dr["所属"] + "</td>");
                        sw.WriteLine("<td valign=\"top\">");
                        if (Convert.ToInt32(dr["事由表示"]) == 0)
                        {
                            sw.WriteLine((string)dr["ゴール"]+"</td><td> " + (string)dr["新記録印刷マーク"]+"</td></tr>");
                        }
                        else
                        {
                            sw.WriteLine((string)dr["棄権印刷マーク"]+"</td></tr>");
                        }

                        string distance = (string)dr["距離"];
                        int numberOfLap = MDBInterface.HowManyLapTimes(distance);
                        string prevLap;
                        int splitCounter;
                        int ithLap;
                        string thisLap;
                        string thisLapMeter;
                        string[] splitTime = new string[5];

                        if (numberOfLap > 1)               
                        {                                  
                            prevLap = "";                  
                            splitCounter = 1;              
                                                            

                            for (ithLap = 1; ithLap <= numberOfLap; ithLap++)
                            {
                               
                                thisLapMeter = GetLap(ithLap, MDBInterface.lapCode);
                                thisLap = (string) dr[thisLapMeter];
                                if (ithLap % 4 == 1) // was 1 now is 0
                                {
                                    sw.WriteLine("<tr> <td colspan=4 align=\"center\"> <div class=\"lap_container\">");
                                }

                                sw.WriteLine("<div class=\"lap_time\">" + thisLap + "</div>");

                                if (ithLap == 1) // was 1 now is 0
                                {
                                    splitTime[splitCounter] = "";
                                }
                                else
                                {
                                    if (thisLap != "")
                                    {
                                        if (prevLap != "")
                                            splitTime[splitCounter] = "(" + Misc.TimeSubtract(thisLap, prevLap) + ")";
                                        else
                                            splitTime[splitCounter] = "";
                                    }
                                    else
                                    {
                                        splitTime[splitCounter] = "";
                                    }

                                }

                                splitCounter++;

                                prevLap = thisLap;

                                if (ithLap % 4 == 0) // was 0 is 3
                                {
                                    sw.WriteLine("</div></td></tr>");
                                    splitCounter = 1;
                                    Misc.PrintSplitTime(sw, splitTime[1], splitTime[2], splitTime[3], splitTime[4]);
                                }
                            }

                            if (ithLap % 4 == 3) //was 3 is 2
                            {
                                sw.WriteLine("<div class=\"lap_time\">  </div><div class=\"lap_time\"> </div></td></tr>");
                                Misc.PrintSplitTime(sw, splitTime[1], splitTime[2], "", "");
                            }
                        }
                    }
                    sw.WriteLine("</table>");
                }
            }
        }



static string HtmlName4Relay(MDBInterface mdb, int[] rswimmer )
        {
                           return mdb.GetSwimmerName(rswimmer[0]) + "&nbsp;&nbsp; " +
                                             mdb.GetSwimmerName(rswimmer[1]) + "<br>" +
                                             mdb.GetSwimmerName(rswimmer[2]) + "&nbsp; &nbsp;" +
                                             mdb.GetSwimmerName(rswimmer[3]) + "</td>";


        }
        static void CreateHTMLProgramFormat(MDBInterface mdb, string srcFile, string indexFile, string rankingFile)
        {

            int maxProgramNo = mdb.MaxProgramNo; // You need to implement GetMaxProgramNo() function

            using (StreamWriter writer = new StreamWriter(srcFile, false, System.Text.Encoding.GetEncoding("shift_jis")))
            {
                PrintHTMLHead2(mdb, writer, 2);

                for (int prgNo = 1; prgNo <= maxProgramNo; prgNo++)
                {
                    int uid = mdb.GetUIDFromPrgNo(prgNo);

                    PrintShumoku(mdb, writer, prgNo);
                    writer.WriteLine("<div class=\"ahtag\" align=\"right\"> <a href=\"" + rankingFile + "#PRGH" + prgNo + "\">ランキング</a>&nbsp;");
                    writer.WriteLine("<a href=\"" + indexFile + "\">種目選択に戻る</a></div>");
                    PrintRaceResult(mdb, writer, prgNo, uid);
                }

                PrintTailAndClose(writer);
            }
        }

        static void PrintRaceResult(MDBInterface mdb, StreamWriter writer, int prgNo, int uid)
        {

            int kumi = 0;

            List<Result> results = new List<Result>();
            mdb.GetResultList(uid, ref results);

            foreach (Result record in results)
            {
                {
                    if (kumi < record.kumi)
                    {
                        if (kumi > 0)
                        {
                            writer.WriteLine("</table>");
                        }

                        kumi = record.kumi;

                        writer.WriteLine("<div class=\"kumi\">" + kumi + "組</div>");
                        writer.WriteLine("<table border=\"0\" width=\"100%\">");
                        writer.WriteLine("<tr><th width=\"4%\"></th><th width=\"30%\"></th><th width=\"40%\"></th>" +
                                              "<th width=\"16%\"></th><th width=\"10%\"></th></tr>");
                    }

                    /* zero lane */
                    int laneNo = record.laneNo;
                    if (mdb.zeroUse) laneNo--;

                    string laneStr = (record.laneNo >= 50) ? "補欠" + (record.laneNo - 49).ToString() : laneNo.ToString();

                    writer.WriteLine("<tr><td align=\"right\">" + laneStr + "</td>");

                    if (record.swimmerID > 0)
                    {
                        writer.WriteLine("<td align=\"left\">&nbsp;");

                        if (Misc.IsRelay(mdb.GetStyleFromPrgNo(prgNo)))
                        {
                            writer.WriteLine(mdb.GetRelayTeamName(record.swimmerID) + "</td><td align=\"left\">" +
                                             mdb.GetSwimmerName(record.rswimmer[0]) + "&nbsp;&nbsp; " +
                                             mdb.GetSwimmerName(record.rswimmer[1]) + "<br>" +
                                             mdb.GetSwimmerName(record.rswimmer[2]) + "&nbsp; &nbsp;" +
                                             mdb.GetSwimmerName(record.rswimmer[3]) + "</td>");
                        }
                        else
                        {
                            writer.Write(mdb.GetSwimmerName(record.swimmerID) + "</td>" +
                                             "<td align=\"left\"> (" + mdb.GetTeamName(record.swimmerID) + ")</td>");
                        }

                        if (record.reasonCode == 0)
                            writer.WriteLine("<td align=\"right\">" + Misc.TimeIntToStr(record.goalTime) + "</td>");
                        else if (record.reasonCode == 4)
                            writer.WriteLine("<td align=\"right\">" + Misc.TimeIntToStr(record.goalTime) + "(op)</td>");
                        else
                            writer.WriteLine("<td align=\"right\">" + CONSTANTS.reason[record.reasonCode] + "</td>");


                        if (record.goalTime > 0)
                        {
                            writer.WriteLine("<td>" + record.newRecord + "</td>");
                            /*
                            if (mdb.GameRecordAvailable && mdb.GetGameRecord(prgNo) > record.goalTime)
                            {
                                writer.WriteLine("<td>大会新</td>");
                            }
                            if (mdb.GameRecordAvailable && mdb.GetGameRecord(prgNo) == record.goalTime)
                            {
                                writer.WriteLine("<td>大会タイ</td>");
                            }
                            */

                        }

                        writer.WriteLine("</tr>");
                    }
                    else
                    {
                        writer.WriteLine("<td>   </td><td> </td></tr>");
                    }
                }
            }

            writer.WriteLine("</table>");
        }

        static void PrintHTMLHead( StreamWriter writer, string eventName, string eventDate, string eventVenue, int fType)
        {
            writer.WriteLine("<?php");
            writer.WriteLine(" header(\"Content-Type: text/html; charset=Shift-JIS\");");
            writer.WriteLine("?>");
            writer.WriteLine("<!DOCTYPE Html><Html>");
            writer.WriteLine("<?php");
            writer.WriteLine(" $qarray = explode(\"&\", $_SERVER['QUERY_STRING']);");
            writer.WriteLine(" list($vname1,$value1) = explode(\"=\",$qarray[0]);");
            writer.WriteLine(" list($vname2,$value2) = explode(\"=\",$qarray[1]);");
            writer.WriteLine(" if (strcmp($vname1,\"prgNo\")==0) {");
            writer.WriteLine("     $prgNo=$value1;");
            writer.WriteLine("     $kumiNo=$value2;");
            writer.WriteLine(" } else {");
            writer.WriteLine("     $kumiNo=$value1;");
            writer.WriteLine("     $prgNo=$value2;");
            writer.WriteLine(" }");
            writer.WriteLine("?>");
            writer.WriteLine("<head> ");
            writer.WriteLine($"<meta charset=\"Shift_JIS\"><title>{eventName} </title>");
            if (fType == 1)
            {
                writer.WriteLine("<link rel=\"stylesheet\" media=\"all\" href=\"css/cman.css\">");
                writer.WriteLine("<script type=\"text/javascript\" src=\"cman.js\"></script>");
            }
            if (fType == 2)
            {
                writer.WriteLine("<link rel=\"stylesheet\" media=\"all\" href=\"css/swim.css\">");
            }
            if (fType == 3)
            {
                writer.WriteLine("<link rel=\"stylesheet\" media=\"all\" href=\"css/swimcall.css\">");
            }
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            if (fType < 3)
            {
                writer.WriteLine($"<h3>{eventName} &nbsp;&nbsp;開催地 : {eventVenue} &nbsp;&nbsp;期日 : {eventDate}</h3>");
            }
            if (thisYouTubeURL != "")
            {
                writer.WriteLine($"<h3><a href=\"{thisYouTubeURL} \">YouTube ライブ配信はこちら</a></h3>");
            }
//            writer.WriteLine($"<h3><a href=\" {extraMessageURL} \">" + extraMessage + "</a></h3>");

        }

        static void PrintHTMLHead2(MDBInterface mdb, StreamWriter writer, int fType)
        {
            writer.WriteLine("<?php");
            writer.WriteLine(" header(\"Content-Type: text/html; charset=Shift-JIS\");");
            writer.WriteLine("?>");
            writer.WriteLine("<!DOCTYPE Html><Html>");
            writer.WriteLine("<?php");
            writer.WriteLine(" $qarray = explode(\"&\", $_SERVER['QUERY_STRING']);");
            writer.WriteLine(" list($vname1,$value1) = explode(\"=\",$qarray[0]);");
            writer.WriteLine(" list($vname2,$value2) = explode(\"=\",$qarray[1]);");
            writer.WriteLine(" if (strcmp($vname1,\"prgNo\")==0) {");
            writer.WriteLine("     $prgNo=$value1;");
            writer.WriteLine("     $kumiNo=$value2;");
            writer.WriteLine(" } else {");
            writer.WriteLine("     $kumiNo=$value1;");
            writer.WriteLine("     $prgNo=$value2;");
            writer.WriteLine(" }");
            writer.WriteLine("?>");
            writer.WriteLine("<head> ");
            writer.WriteLine($"<meta charset=\"Shift_JIS\"><title>{MDBInterface.GetEventName()} </title>");
            if (fType == 1)
            {
                writer.WriteLine("<link rel=\"stylesheet\" media=\"all\" href=\"css/cman.css\">");
                writer.WriteLine("<script type=\"text/javascript\" src=\"cman.js\"></script>");
            }
            if (fType == 2)
            {
                writer.WriteLine("<link rel=\"stylesheet\" media=\"all\" href=\"css/swim.css\">");
            }
            if (fType == 3)
            {
                writer.WriteLine("<link rel=\"stylesheet\" media=\"all\" href=\"css/swimcall.css\">");
            }
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            if (fType < 3)
            {
                writer.WriteLine($"<h3>{MDBInterface.GetEventName()} &nbsp;&nbsp;開催地 : {MDBInterface.GetEventVenue()} &nbsp;&nbsp;期日 : {MDBInterface.GetEventDate()}</h3>");
            }
            if (thisYouTubeURL != "")
            {
                writer.WriteLine($"<h3><a href=\"{thisYouTubeURL} \">YouTube ライブ配信はこちら</a></h3>");
            }
//            writer.WriteLine($"<h3><a href=\" {extraMessageURL} \">" + extraMessage + "</a></h3>");

        }
        static void CreateIndexHTML(MDBInterface mdb, string myName, string rankingFile, string kanproFile)
        {
            int prgNo;
            int maxPrgNo;
            int uid;

            using (StreamWriter writer = new StreamWriter(myName, false, System.Text.Encoding.GetEncoding("shift_jis")))
            {
                PrintHTMLHead2(mdb, writer, 1);
                writer.WriteLine("<table id=\"sampleTable\" border=\"0\" width=\"95%\">");
                writer.WriteLine("<tr><th cmanFilterBtn>競技番号</th><th cmanFilterBtn>クラス</th>" +
                                 "<th cmanFilterBtn>性別</th><th cmanFilterBtn>距離</th>" +
                                 "<th cmanFilterBtn>種目</th><th cmanFilterBtn>予/決</th><th>  </th><th>  </th></tr>");
                maxPrgNo = mdb.MaxProgramNo;

                for (prgNo = 1; prgNo <= maxPrgNo; prgNo++)
                {
                    uid = mdb.GetUIDFromPrgNo(prgNo);
                    if (uid != 0)
                    {
                        writer.WriteLine($"<tr><td align=\"right\">{prgNo}</td>");
                        writer.WriteLine($"<td align=\"left\">{mdb.GetClassFromPrgNo(prgNo)}</td>");
                        writer.WriteLine($"<td align=\"center\">{mdb.GetGenderFromPrgNo(prgNo)}</td> ");
                        writer.WriteLine($"<td align=\"right\">{mdb.GetDistanceFromPrgNo(prgNo)}</td>");
                        writer.WriteLine($"<td align=\"left\">{mdb.GetStyleFromPrgNo(prgNo)}</td>");
                        writer.WriteLine($"<td align=\"left\">{mdb.GetPhaseFromPrgNo(prgNo)}</td>");
                        writer.WriteLine($"<td> <a href=\"{kanproFile}#PRGH{prgNo}\"> レーン順</a></td>");
                        writer.WriteLine($"<td> <a href=\"{rankingFile}#PRGH{prgNo}\"> 順位表</a></td></tr>");
                    }
                }

                writer.WriteLine("</table>");
                PrintTailAndClose(writer);
            }
        }

        static void PrintTailAndClose(StreamWriter writer)
        {
            // Implement your logic for printing HTML tail and closing the writer
            writer.WriteLine("<br><br><br><br><br>");
            writer.WriteLine($"<div class=\"footer\" align=\"right\"> updated by {Dns.GetHostName()} at {DateTime.Now} </div>");
            writer.WriteLine("</body></Html>");
            writer.Close();
        }

    }
    public class MDBInterface
    {
        const int NORECORDYET = 0;
        private List<Result> resultList;



        public Result GetResult(int rn) { return resultList[rn]; }
        public void GetResultList(int prgNo, ref List<Result> extracted)
        {
            foreach (Result result in resultList)
            {
                if (result.uid == prgNo)
                {
                    extracted.Add(result);
                }
            }
        }
        private readonly int eventNo;
        public static string SERVERName;

        private readonly string[] ShumokuTable = new string[8];
        public string GetShumoku(int id) { return ShumokuTable[id]; }
        private readonly string[] DistanceTable = new string[11];
        private readonly string[] YoketsuTable = new string[9];

        private const string magicWord = "\\SQLEXPRESS;User ID=Sw;Password=;Database=Sw;TrustServerCertificate=True;Encrypt=True";
        private const string magicHead = "Server=";
        private string[] genderStr;
        public bool GameRecordAvailable;
        private string[] swimmerName;
        public string GetSwimmerName(int id) { return swimmerName[id]; }
        private string[] kana;
        private int[] belongsTo;
        public string GetTeamName(int swimmerID) { return clubName[belongsTo[swimmerID]]; }
        private string[] clubName;

        static public bool ServerAccessOK(string serverName)
        {
            string connectionString = magicHead + serverName + magicWord;
            string sqlQuery = "select * from クラス";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                            }
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        private readonly int maxProgramNo = 0;
        public int MaxProgramNo
        {
            get { return maxProgramNo; }
        }
        private readonly int maxUID = 0;
        public int MaxUID
        {
            get { return maxUID; }
        }
        int touchBoard; // 1--50m  2--100m  3--25m   4--50m 
        public bool zeroUse { get; set; }
        public static int lapCode { get; set; }

        private int[] UIDFromPrgNo;
        int[] ClassNoByPrgNo;
        int[] genderByPrgNo;　　// 男子, 女子, 混合
        int[] styleByPrgNo;
        int[] distanceByPrgNo;
        string[] phaseByPrgNo; // 予選/決勝/タイム決勝
        static int[] gameRecord4PrgNo;
        static public int GetGameRecord(int prgNo) { return gameRecord4PrgNo[prgNo]; }
        int[] numSwimmers4UID;
        public int GetHowManySwimmers(int uid) { return numSwimmers4UID[uid]; }
        public int GetUIDFromPrgNo(int prgNo) { return UIDFromPrgNo[prgNo]; }

        public string GetClassFromPrgNo(int uid) { return className[ClassNoByPrgNo[uid]]; }
        public string GetGenderFromPrgNo(int uid) { return genderStr[genderByPrgNo[uid]]; }
        public string GetStyleFromPrgNo(int uid) { return ShumokuTable[styleByPrgNo[uid]]; }
        public string GetDistanceFromPrgNo(int uid) { return DistanceTable[distanceByPrgNo[uid]]; }
        public int GetDistanceCodeFromPrgNo(int uid) { return distanceByPrgNo[uid]; }
        public string GetPhaseFromPrgNo(int uid) { return phaseByPrgNo[uid]; }


        private readonly string[] TeamName4Relay;
        public string GetRelayTeamName(int id)
        {
            return TeamName4Relay[id];
        }
#nullable enable
        public MDBInterface(string serverName, int eventNo)
        {
            this.eventNo = eventNo;
            SERVERName = serverName;
            InitTables();
            resultList = new List<Result>();
            InitProgramDB(ref maxProgramNo, ref maxUID);
            RedimProgramDBArrays(maxProgramNo, maxUID);
            InitClassDB();
            clubName = new string[10];
            InitClubName();
            RedimSwimmerDBArrays(GetNumSwimmers());
            TeamName4Relay = new string[GetNumRelayTeams() + 1];
            ReadMDB();
        }

        public static int HowManyLapTimes(string distance)
        {
            if ( distance =="  25m")
                //if (lapCode == 1) return 1;
                return 0;
            if ( distance =="  50m") { 
                if (lapCode == 4) return 0;
                if (lapCode == 1) return 2;
                return 1;
            }
            if (distance== " 100m")
            {

                if (lapCode == 4) return 1;
                if (lapCode == 1) return 4;
                return 2;
            }
            if (distance==" 200m")
            {

                if (lapCode == 4) return 2;
                if (lapCode == 1) return 8;
                return 4;
            }
            if (distance==" 400m")
            {

                if (lapCode == 4) return 4;
                if (lapCode == 1) return 16;
                return 8;
            }
            if (distance==" 800m")
            {

                if (lapCode == 4) return 8;
                if (lapCode == 1) return 32;
                return 16;
            }
            if (distance=="1500m")
            {

                if (lapCode == 4) return 15;
                if (lapCode == 1) return 60;
                return 30;
            }
                return 0; // Default to 0 if distance is not recognized
        }


        public void GetResultNo(ref List<int> recordNums, int uid, int rank)
        {
            for (int i = 0; i < resultList.Count; i++)
            {
                if ((resultList[i].uid == uid) && (resultList[i].rank == rank))
                {
                    recordNums.Add(i);
                }
            }
        }
        void ReadMDB()
        {
            ReadEventDB();
            ReadTeamDB();
            ReadClassDB();
            ReadSwimmerDB();
            ReadProgramDB();
            ReadResultDB();
            ReadRecordDB();

            AnalyzeResult();
        }
        /*-----------------get_lap_interval-------*/
        void InitLapInterval()
        {

            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT タッチ板 FROM 大会設定 where 大会番号=" + eventNo + ";";

                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            touchBoard = Misc.Obj2Int(dr["タッチ板"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (touchBoard == 3) lapCode = 1;
                if ((touchBoard == 1) || (touchBoard == 4)) lapCode = 2;
                if (touchBoard == 2) lapCode = 4;
            }
        }
        /*
                   touchBoard   lapCode  lapInterval
        長水路両側   1           2           50m毎
        長水路片側   2           4          100m
        短水路両側   3           1           25m
        短水路片側   4           2           50m
         */

        void InitStyleTable()
        {
            /*
            ShumokuTable[0] = "";
            ShumokuTable[1] = "自由形";
            ShumokuTable[2] = "背泳ぎ";
            ShumokuTable[3] = "平泳ぎ";
            ShumokuTable[4] = "バタフライ";
            ShumokuTable[5] = "個人メドレー";
            ShumokuTable[6] = "リレー";
            ShumokuTable[7] = "メドレーリレー";
            */
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT 種目, 種目コード FROM 種目 ;";

                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ShumokuTable[Misc.Obj2Int(dr["種目コード"])] = Misc.Obj2String(dr["種目"]).Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void InitYoketsuTable()
        {
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT 予決コード, 予決 FROM 予決;";

                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int ycode = Misc.Obj2Int(dr["予決コード"]);
                            YoketsuTable[ycode] = Misc.Obj2String(dr["予決"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static bool ClassExist=false;
        void InitDistanceTable()
        {
            /*
            DistanceTable[0] = "";
            DistanceTable[1] = "  25m";
            DistanceTable[2] = "  50m";
            DistanceTable[3] = " 100m";
            DistanceTable[4] = " 200m";
            DistanceTable[5] = " 400m";
            DistanceTable[6] = " 800m";
            DistanceTable[7] = "1500m";
            */

            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT 距離コード, 距離 FROM 距離;";

                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int distance = Misc.Obj2Int(dr["距離コード"]);
                            DistanceTable[distance] = Misc.Obj2String(dr["距離"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        int LocateDistanceID(string distance)
        {
            for (int cnt = 1; cnt < 8; cnt++)
            {
                if (DistanceTable[cnt] == distance) return cnt;
            }
            return 0; //error
        }
        void InitTables()
        {
            genderStr = new string[5] { "", "男子", "女子", "混成", "混合" };
            InitLapInterval();
            InitStyleTable();
            InitDistanceTable();
            InitYoketsuTable();
        }
        public bool RaceExist(int uid)
        {
            for (int cnt = 0; cnt < resultList.Count; cnt++)
            {
                if (resultList[cnt].uid == uid) return true;
            }
            return false;
        }
        string[] className;
        void InitClassDB()
        {
            int numClasses = 0;
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT MAX(クラス番号) as maxClass FROM dbo.クラス where 大会番号=" + eventNo + ";";

                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        dr.Read();
                        numClasses = Misc.Obj2Int(dr["maxClass"]);
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            className = new string[numClasses + 1];
            if (numClasses > 0)
            {
                ClassExist = true;
            }
        }


        void ReadRecordDB()
        {
            using (SqlConnection connection = new SqlConnection(magicHead + SERVERName + magicWord))
            {
                try
                {

                    connection.Open();

                    string query = @"SELECT プログラム.競技番号 as UID, プログラム.表示用競技番号 as PrgNo, 新記録.記録  as 記録 FROM プログラム 
                                   INNER JOIN 新記録 ON プログラム.種目コード = 新記録.種目コード 
                                   AND プログラム.距離コード = 新記録.距離コード 
                                   AND プログラム.クラス番号 = 新記録.記録区分番号 
                                   AND プログラム.性別コード = 新記録.性別コード 
                                   WHERE プログラム.大会番号= @eventNo and 新記録.大会番号=@eventNo;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@eventNo", SqlDbType.Int).Value = eventNo;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                GameRecordAvailable = true;
                                while (reader.Read())
                                {
                                    int uid = Convert.ToInt32(reader["UID"]);
                                    int prgNo = Convert.ToInt32(reader["PrgNo"]);
                                    object recordValue = reader["記録"];
                                    if (recordValue == DBNull.Value)
                                    {
                                        gameRecord4PrgNo[prgNo] = NORECORDYET;
                                    }
                                    else
                                    {
                                        gameRecord4PrgNo[prgNo] = Misc.TimeStrToInt(recordValue.ToString().Trim());
                                    }

                                }
                            }
                            else
                            {
                                GameRecordAvailable = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        void ReadClassDB()
        {
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT クラス番号,クラス名称 FROM クラス where 大会番号 =  @eventNo ;";

                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    comm.Parameters.Add("@eventNo", SqlDbType.Int).Value = eventNo;
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            className[Misc.Obj2Int(dr["クラス番号"])] = Misc.Obj2String(dr["クラス名称"]).Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void InitProgramDB(ref int maxProgramNo, ref int maxUID)
        {
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT MAX(表示用競技番号) AS MAXPRGNO, MAX(競技番号) AS MAXUID FROM プログラム WHERE 大会番号=" + eventNo + ";";
                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        dr.Read();
                        maxUID = Misc.Obj2Int(dr["MAXUID"]);
                        maxProgramNo = Misc.Obj2Int(dr["MAXPRGNO"]);
                    }
                }
                catch
                {
                    MessageBox.Show("error in InitProgramDB ... Database error\n ");
                }
            }
        }
        void RedimProgramDBArrays(int maxPrgNo, int maxUID)
        {
            maxPrgNo++;
            maxUID++;
            UIDFromPrgNo = new int[maxPrgNo];
            genderByPrgNo = new int[maxPrgNo];
            styleByPrgNo = new int[maxPrgNo];
            distanceByPrgNo = new int[maxPrgNo];
            phaseByPrgNo = new string[maxPrgNo];
            ClassNoByPrgNo = new int[maxPrgNo];
            gameRecord4PrgNo = new int[maxPrgNo];
            numSwimmers4UID = new int[maxUID];
        }
        void ReadProgramDB()
        {
            int uid;
            int prgNo;

            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT  競技番号, 表示用競技番号, 種目コード, 距離コード, " +
                    "性別コード, 予決コード, クラス番号, ポイント設定番号 FROM プログラム where 大会番号=" + eventNo + ";";
                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            uid = Misc.Obj2Int(dr["競技番号"]);
                            prgNo = Misc.Obj2Int(dr["表示用競技番号"]);
                            UIDFromPrgNo[prgNo] = uid;
                            ClassNoByPrgNo[prgNo] = Misc.Obj2Int(dr["クラス番号"]);
                            genderByPrgNo[prgNo] = Misc.Obj2Int(dr["性別コード"]);
                            styleByPrgNo[prgNo] = Misc.Obj2Int(dr["種目コード"]);
                            distanceByPrgNo[prgNo] = Misc.Obj2Int(dr["距離コード"]);
                            phaseByPrgNo[prgNo] = YoketsuTable[Misc.Obj2Int(dr["予決コード"])];
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        static string eventName;
        static string eventDate;
        static string eventVenue;
        public static string GetEventName() { return eventName; }
        public static string GetEventDate() { return eventDate; }
        public static string GetEventVenue() { return eventVenue; }
        void ReadEventDB()
        {
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT 大会名１,開催地,始期間,終期間, ゼロコース使用 FROM 大会設定 where 大会番号=" + eventNo + ";";
                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        dr.Read();
                        eventName = Misc.Obj2String(dr["大会名1"]);
                        if ((Misc.Obj2String(dr["始期間"])).Equals(Misc.Obj2String(dr["終期間"])))
                        {
                            eventDate = Misc.Obj2String(dr["始期間"]);
                        }
                        else
                        {
                            eventDate = Misc.Obj2String(dr["始期間"]) + "～" + Misc.Obj2String(dr["終期間"]);
                        }
                        eventVenue = Misc.Obj2String(dr["開催地"]);
                        zeroUse = Convert.ToBoolean(dr["ゼロコース使用"]);

                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        int GetNumRelayTeams()
        {
            int numRelayTeams = 0;
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT MAX(チーム番号) AS MAXRTEAMNUM FROM リレーチーム;";
                SqlCommand comm = new SqlCommand(myQuery, conn);

                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        dr.Read();
                        numRelayTeams = Misc.Obj2Int(dr["MAXRTEAMNUM"]);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            return numRelayTeams;
        }
        void ReadTeamDB()
        {
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT チーム番号,チーム名 FROM リレーチーム where 大会番号=" + eventNo + ";";
                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TeamName4Relay[Misc.Obj2Int(dr["チーム番号"])] = Misc.Obj2String(dr["チーム名"]);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        int GetNumSwimmers()
        {
            int numSwimmers = 0;
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT MAX(選手番号) AS MAXSNUM FROM 選手 where 大会番号=" + eventNo + ";";
                SqlCommand comm = new SqlCommand(myQuery, conn);

                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        dr.Read();
                        numSwimmers = Misc.Obj2Int(dr["MAXSNUM"]);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            return numSwimmers;
        }

        void RedimSwimmerDBArrays(int maxnum)
        {
            maxnum++;
            swimmerName = new string[maxnum];
            kana = new string[maxnum];
            belongsTo = new int[maxnum];
        }
        int numTeams = 0;
        int LocateTeamID(string teamName)
        {
            int team_id;
            for (team_id = 1; team_id <= numTeams; team_id++)
            {
                if (clubName[team_id] == teamName) return team_id;
            }
            numTeams = team_id;
            throw new ArgumentException("error in LocateTeamID. InitCoubName has a kind of bug.");

        }
        int InitClubName()
        {
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                string myQuery = "SELECT DISTINCT 所属名称１ AS CLUBNAME FROM 選手 where 大会番号=" + eventNo + ";";
                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            numTeams++;
                            if (numTeams >= clubName.Length) Array.Resize(ref clubName, numTeams + 1);
                            clubName[numTeams] = dr.GetString(0).Trim();
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            return (numTeams);
        }
        void ReadSwimmerDB()
        {
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                int clubNo;
                int swimmerID = 0;
                string myQuery = "SELECT 選手番号, 氏名カナ, 氏名, 所属番号１, 所属名称１ FROM 選手 where 大会番号= " + eventNo + ";";
                SqlCommand comm = new SqlCommand(myQuery, conn);
                try
                {
                    conn.Open();
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clubNo = LocateTeamID(Misc.Obj2String(dr["所属名称1"]).Trim());
                            swimmerID = Misc.Obj2Int(dr["選手番号"]);
                            swimmerName[swimmerID] =
                                Misc.Obj2String(dr["氏名"]).TrimEnd();
                            kana[swimmerID] = Misc.Obj2String(dr["氏名カナ"]).TrimEnd();
                            belongsTo[swimmerID] = clubNo;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        class Swimmer
        {
            public int swimmerID { get; set; }
            public int goalTime { get; set; }
            public string lapStr { get; set; }
            public int rank { get; set; }
        }




        void ReadResultDB()
        {
            string[] lapstring = {"lap25","lap50","lap75","lap100","lap125","lap150","lap175", "lap200",
                                "lap225","lap250", "lap275", "lap300","lap325", "lap350", "lap375","lap400",
                                "lap425", "lap450", "lap475", "lap500", "lap525", "lap550", "lap575", "lap600",
                                "lap625", "lap650", "lap675", "lap700", "lap725", "lap750", "lap775", "lap800",
                                "lap825", "lap850", "lap875", "lap900", "lap925", "lap950", "lap975", "lap1000",
                                "lap1025", "lap1050", "lap1075", "lap1100", "lap1125", "lap1150", "lap1175", "lap1200",
                                "lap1225", "lap1250", "lap1275", "lap1300", "lap1325", "lap1350", "lap1375", "lap1400",
                                "lap1425", "lap1450", "lap1475", "lap1500" };
            SqlConnection conn = new SqlConnection(magicHead + SERVERName + magicWord);
            using (conn)
            {
                Result result;
                string myQuery = @"SELECT  DISTINCT 記録.競技番号 as UID ,ゴール, 選手番号,  
                  第１泳者, 第２泳者, 第３泳者, 第４泳者,  
                   ラップ.[25m] as lap25,  ラップ.[50m] as lap50, ラップ.[75m] as lap75, ラップ.[100m] as lap100, 
                    ラップ.[125m] as lap125, ラップ.[150m] as lap150, ラップ.[175m] as lap175, ラップ.[200m] as lap200, 
                    ラップ.[225m] as lap225, ラップ.[250m] as lap250, ラップ.[275m] as lap275, ラップ.[300m] as lap300, 
                    ラップ.[325m] as lap325, ラップ.[350m] as lap350, ラップ.[375m] as lap375, ラップ.[400m] as lap400, 
                    ラップ.[425m] as lap425, ラップ.[450m] as lap450, ラップ.[475m] as lap475, ラップ.[500m] as lap500, 
                    ラップ.[525m] as lap525, ラップ.[550m] as lap550, ラップ.[575m] as lap575, ラップ.[600m] as lap600, 
                    ラップ.[625m] as lap625, ラップ.[650m] as lap650, ラップ.[675m] as lap675, ラップ.[700m] as lap700, 
                    ラップ.[725m] as lap725, ラップ.[750m] as lap750, ラップ.[775m] as lap775, ラップ.[800m] as lap800, 
                    ラップ.[825m] as lap825, ラップ.[850m] as lap850, ラップ.[875m] as lap875, ラップ.[900m] as lap900, 
                    ラップ.[925m] as lap925, ラップ.[950m] as lap950, ラップ.[975m] as lap975, ラップ.[1000m] as lap1000, 
                    ラップ.[1025m] as lap1025, ラップ.[1050m] as lap1050, ラップ.[1075m] as lap1075, ラップ.[1100m] as lap1100, 
                    ラップ.[1125m] as lap1125, ラップ.[1150m] as lap1150, ラップ.[1175m] as lap1175, ラップ.[1200m] as lap1200, 
                    ラップ.[1225m] as lap1225, ラップ.[1250m] as lap1250, ラップ.[1275m] as lap1275, ラップ.[1300m] as lap1300, 
                    ラップ.[1325m] as lap1325, ラップ.[1350m] as lap1350, ラップ.[1375m] as lap1375, ラップ.[1400m] as lap1400, 
                    ラップ.[1425m] as lap1425, ラップ.[1450m] as lap1450, ラップ.[1475m] as lap1475, ラップ.[1500m] as lap1500, 
                   記録.組 as 組, 記録.水路 as 水路, 事由入力ステータス, 新記録印刷マーク 
                  FROM 記録 
                   INNER JOIN ラップ ON 記録.競技番号 = ラップ.競技番号 AND 
                   記録.組 = ラップ.組 AND 記録.水路 = ラップ.水路 
                   where 記録.大会番号= @eventNo  AND ラップ.大会番号 =  @eventNo 
                    and ラップ.ラップ区分=0
                 　ORDER BY UID, 組, 水路 ; ";
                SqlCommand comm = new SqlCommand(myQuery, conn);
                //   try
                //   {
                comm.Parameters.Add("@eventNo", SqlDbType.Int).Value = eventNo;
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (Misc.Obj2Int(dr["UID"])<=maxUID)
                        //if (Misc.Obj2Int(dr["UID"])>0)
                        //if (Misc.Obj2Int(dr["選手番号"])>0)
                        {
                            result = new Result();
                            result.uid = Misc.Obj2Int(dr["UID"]);
                            numSwimmers4UID[result.uid]++;  // sometimes uid gets bigger than maxuid
                            result.kumi = Misc.Obj2Int(dr["組"]);
                            result.swimmerID = Misc.Obj2Int(dr["選手番号"]);
                            result.rswimmer[0] = Misc.Obj2Int(dr["第１泳者"]);
                            result.rswimmer[1] = Misc.Obj2Int(dr["第２泳者"]);
                            result.rswimmer[2] = Misc.Obj2Int(dr["第３泳者"]);
                            result.rswimmer[3] = Misc.Obj2Int(dr["第４泳者"]);
                            result.laneNo = Misc.Obj2Int(dr["水路"]);
                            result.reasonCode = Misc.Obj2Int(dr["事由入力ステータス"]);
                            result.newRecord = Misc.Obj2String(dr["新記録印刷マーク"]);
                            for (int i = 0; i < 60; i++)
                            {
                                result.lapTime[i] = Misc.Obj2String(dr[lapstring[i]]);

                            }
                            if ((result.reasonCode == 0) || (result.reasonCode == 4))
                            {
                                result.goalTime = Misc.TimeStrToInt(Misc.Obj2String(dr["ゴール"]));
                            } else
                            if (result.reasonCode == CONSTANTS.DQ)
                            {
                                result.goalTime = CONSTANTS.TIME4DQ;
                            } else
                            if (result.reasonCode == CONSTANTS.DNS)
                            {
                                result.goalTime = CONSTANTS.TIME4DNS;
                            } else 
                            if (result.reasonCode == CONSTANTS.DNSM)
                            {
                                result.goalTime = CONSTANTS.TIME4DNSM;
                            }
                            result.rank = 1;
                            resultList.Add(result);

                        }
                    }
                }
                // }
                // catch (Exception ex) {
                //     MessageBox.Show(ex.Message);
                // }
            }
        }


        void AnalyzeResult()
        {
            int uid;
            for (uid = 1; uid <= MaxUID; uid++)
            {
                int myStart = 1, myEnd = resultList.Count, flag;
                flag = 0;
                for (int j = 0; j < resultList.Count; j++)
                {
                    if (flag == 0)
                    {
                        if (resultList[j].uid == uid)
                        {
                            myStart = j;
                            flag = 1;
                        }
                    }
                    if (resultList[j].uid > uid)
                    {
                        myEnd = j;
                        break;
                    }
                }
                if (flag == 1)
                {
                    for (int j = myStart; j < myEnd; j++)
                    {
                        int myTime = resultList[j].goalTime;
                        for (int k = myStart; k < myEnd; k++)
                        {
                            if (resultList[k].goalTime < myTime) resultList[j].rank++;
                        }
                    }
                }
            }
        }


    }
    public static class Misc
    {
        static string orgstrSave = string.Empty;
        const int ELEMENTLENGTH = 18;

        public static string ParseLap(string orgstr)
        {
            if (!string.IsNullOrEmpty(orgstr))
            {
                orgstrSave = orgstr;
            }

            string parsedLap = orgstrSave.Substring(0, Math.Min(ELEMENTLENGTH, orgstrSave.Length)).Trim();
            orgstrSave = orgstrSave.Substring(Math.Min(ELEMENTLENGTH, orgstrSave.Length));

            return parsedLap;
        }

        public static bool IsDQorDNS(int code) { return (code >= 1); }
        public static string TimeSubtract(string time1, string time2)
        {
            if (time1 == "" || time1 == " ") return "";
            if (time2 == "" || time2 == " ") return "";
            long intTime1 = Str2Milliseconds(time1);
            long intTime2 = Str2Milliseconds(time2);
            long answer = intTime1 - intTime2;
            return Milliseconds2Str(answer);
        }

        static string Milliseconds2Str(long milliseconds)
        {
            long myMinutes = milliseconds / 6000;
            string minutesStr = (myMinutes > 0) ? $"{myMinutes:D2}:" : "";
            long rest = milliseconds % 6000;
            string secondStr = $"{rest / 100:D2}";
            string millisecondStr = $"{rest % 100:D2}";
            return $"{minutesStr}{secondStr}.{millisecondStr}";
        }

        static long Str2Milliseconds(string myTime)
        {
            if (myTime == "") return 0;
            long millisecond;
            long myMinutes;
            try { 
                int colonPos = myTime.IndexOf(":");
                if (colonPos > 0)
                {
                    myMinutes = long.Parse(myTime.Substring(0, colonPos));
                    millisecond = long.Parse(myTime.Substring(colonPos + 1, 5).Replace(".", ""));
                }
                else
                {
                    myMinutes = 0;
                    millisecond = long.Parse(myTime.Replace(".", ""));
                }

                return 6000 * myMinutes + millisecond;
            }
            catch { return 0; }
        }


        public static void PrintSplitTime(StreamWriter sw, string time1, string time2, string time3, string time4)
        {
            sw.WriteLine("<tr><td colspan=4 align=\"center\"><div class=\"lapcontainer\">");
            sw.WriteLine("<div class=\"lap_time\">" + time1 + "</div>");
            sw.WriteLine("<div class=\"lap_time\">" + time2 + "</div>");
            sw.WriteLine("<div class=\"lap_time\">" + time3 + "</div>");
            sw.WriteLine("<div class=\"lap_time\">" + time4 + "</div>");
            sw.WriteLine("</div></td></tr>");
        }
        public static bool IsRelay(string style)
        {
            return style.Contains("リレー");
        }
        public static bool IsRelay(int styleNo)
        {
            return (styleNo > 5);
        }
        public static string RecordConcatenate(object stra, object strb, object strc)
        {
            string concatenatedString = "";
            if (stra != null && stra != DBNull.Value)
            {
                concatenatedString = stra.ToString();
            }
            if (strb != null && strb != DBNull.Value)
            {
                concatenatedString += strb.ToString();
            }
            if (strc != null && strc != DBNull.Value)
            {
                concatenatedString += strc.ToString();
            }
            return concatenatedString;
        }
        public static int TimeStrToInt(string timeStr)
        {
            if (string.IsNullOrWhiteSpace(timeStr)) return CONSTANTS.TIME4DQ;

            string workStr = timeStr.Replace(":", "").Replace(".", "");
            return Convert.ToInt32(workStr);
        }



        public static string TimeIntToStr(int mytime)
        {
            string minutes;
            string seconds;
            string centiSecond;
            int mytimeCP;

            if ((mytime == 0) || (mytime > 995999))
            {
                return "";
            }

            mytimeCP = mytime;

            minutes = Right(" " + Convert.ToString((int)(mytimeCP / 10000)), 2);
            mytimeCP = mytimeCP % 10000;
            seconds = Right("0" + Convert.ToString((int)(mytimeCP / 100)), 2);
            mytimeCP = mytimeCP % 100;
            centiSecond = Right("0" + Convert.ToString(mytimeCP), 2);

            if (mytime >= 10000)
            {
                return minutes + ":" + seconds + "." + centiSecond;
            }
            else
            {
                return "   " + seconds + "." + centiSecond;
            }
        }
        static string Right(string value, int length)
        {
            if (value.Length <= length)
            {
                return value;
            }
            else
            {
                return value.Substring(value.Length - length);
            }
        }

        public static void ReadConfigFile(string filename,
            ref string serverName,
            ref string eventNoStr,
            ref string htmlFilePath,
            ref string indexFile,
            ref string kanproFile,
            ref string rankingFile,
            ref string scoreFile,
            ref string hostName,
            ref string port,
            ref string userName,
            ref string keyFile)
        {
            XDocument xml = XDocument.Load(filename);
            serverName = xml.Root.Element("serverName").Value;
            eventNoStr = xml.Root.Element("eventNo").Value;
            htmlFilePath = xml.Root.Element("htmlFilePath").Value;
            indexFile = xml.Root.Element("indexFile").Value;
            kanproFile = xml.Root.Element("kanproFile").Value;
            rankingFile = xml.Root.Element("rankingFile").Value;
            scoreFile = xml.Root.Element("scoreFile").Value;
            hostName = xml.Root.Element("hostName").Value;
            port = xml.Root.Element("port").Value;
            userName = xml.Root.Element("userName").Value;
            keyFile = xml.Root.Element("keyFile").Value;
        }


        public static void ReadIniFile(string filename,
            ref string serverName,
            ref string eventNoStr,
             ref string htmlFilePath,
            ref string indexFile, ref string kanproFile,
            ref string rankingFile, ref string scoreFile,
            ref string hostName, ref string port,
            ref string userName, ref string keyFile)
        {

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            try
            {

                using (StreamReader reader = new StreamReader(filename, System.Text.Encoding.GetEncoding("shift_jis")))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#") continue;
                        string[] words = line.Split('>');
                        if (words[0] == "ServerName") serverName = words[1];
                        if (words[0] == "eventNo") eventNoStr = words[1];
                        if (words[0] == "htmlFilePath") htmlFilePath = words[1];
                        if (words[0] == "indexFile") indexFile = words[1];
                        if (words[0] == "kanproFile") kanproFile = words[1];
                        if (words[0] == "rankingFile") rankingFile = words[1];
                        if (words[0] == "scoreFile") scoreFile = words[1];
                        if (words[0] == "hostName") hostName = words[1];
                        if (words[0] == "port") port = words[1];
                        if (words[0] == "userName") userName = words[1];
                        if (words[0] == "keyFile") keyFile = words[1];
                    }
                }
            }
            catch
            {
                MessageBox.Show("cannot find " + filename);
            }
        }


        public static void WriteConfigFile(
            string filename,     // CreateWebReport.ini
                                          string serverName,  // C:Users\ykato\OneDrive\SwimDB\DB2024\Swim32.mdb 
                                          string eventNo,
                                          string htmlFilePath, // usually rFlash/xxxx
                                          string indexFile,    // xxxx.html
                                          string kanproFile,   // xxxxp.html
                                          string rankingFile,  // xxxxr.html
                                          string scoreFile,
                                          string hostName,
                                          string port,
                                          string userName,
                                          string keyFile)
        {

            XDocument xml = XDocument.Load(filename);
            xml.Root.Element("serverName").Value = serverName;
            xml.Root.Element("eventNo").Value = eventNo;
            xml.Root.Element("htmlFilePath").Value = htmlFilePath;
            xml.Root.Element("indexFile").Value = indexFile;
            xml.Root.Element("kanproFile").Value = kanproFile;
            xml.Root.Element("rankingFile").Value = rankingFile;
            xml.Root.Element("scoreFile").Value = scoreFile;
            xml.Root.Element("hostName").Value = hostName;
            xml.Root.Element("port").Value = port;
            xml.Root.Element("userName").Value = userName;
            xml.Root.Element("keyFile").Value = keyFile;
            xml.Save(filename)  ;

        }


        public static void WriteIniFile(string filename,     // CreateWebReport.ini
                                          string serverName,  // C:Users\ykato\OneDrive\SwimDB\DB2024\Swim32.mdb 
                                          string eventNo,
                                          string htmlFilePath, // usually rFlash/xxxx
                                          string indexFile,    // xxxx.html
                                          string kanproFile,   // xxxxp.html
                                          string rankingFile,  // xxxxr.html
                                          string scoreFile,
                                          string hostName,
                                          string port,
                                          string userName,
                                          string keyFile)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.GetEncoding("shift_jis")))
            {
                sw.WriteLine($"ServerName>{serverName}");
                sw.WriteLine($"eventNo>{eventNo}");
                sw.WriteLine($"htmlFilePath>{htmlFilePath}");
                sw.WriteLine($"indexFile>{indexFile}");
                sw.WriteLine($"kanproFile>{kanproFile}");
                sw.WriteLine($"rankingFile>{rankingFile}");
                sw.WriteLine($"scoreFile>{scoreFile}");
                sw.WriteLine($"hostName>{hostName}");
                sw.WriteLine($"port>{port}");
                sw.WriteLine($"userName>{userName}");
                sw.WriteLine($"keyFile>{keyFile}");
            }

        }
        //
        /// <summary>
        /// SendFile sends source file to webserver.
        /// </summary>
        /// <param name="source">Source html file which is to be sent to the server(host).</param>
        /// <param name="destination">server directory in which source html file is stored</param>
        /// <param name="hostName">web server host name or ip address. Default: otsuswim.ddns.net</param>
        /// <param name="port">web server ssh port number (usually 22) </param>
        /// <param name="userName">user name of hte server. Default: www-data</param>
        /// <param name="keyFile">Secret file that corresponds to the server public key.</param>
        // 
        public static void SendFile(string source, string destination, string hostName, int port, string userName,
            string keyFile)
        {
            if (hostName == "") return;

            try
            {
                var connectionInfo = new PrivateKeyConnectionInfo(hostName, port, userName, new PrivateKeyFile(keyFile));

                FileInfo fi = new FileInfo(source);
                using (var client = new ScpClient(connectionInfo))
                {
                    client.Connect();

                    using (var fileStream = fi.OpenRead())
                    {
                        client.Upload(fileStream, destination);

                    }

                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + "Web serverにはsend しませんでした。");
            }
        }
        public static string Obj2String(object obj)
        {
            if (obj == DBNull.Value) return string.Empty;
            return (string)obj;
        }
        public static int Obj2Int(object obj)
        {
            if (obj != DBNull.Value) return Convert.ToInt32(obj);
            return 0;
        }
        public static string GetFilePath(string title = "ファイルを選択してください",
                            string initFolder = @"C:\",
                            string option = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*"
                            )
        {
            string selectedFilePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // ダイアログのタイトルを設定
            openFileDialog.Title = title;

            // 初期ディレクトリを設定（オプション）
            openFileDialog.InitialDirectory = initFolder;
            // initFolder=@"C:\"

            // ファイルの種類を指定（オプション）
            openFileDialog.Filter = option;

            // ユーザーが選択したファイルを取得
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
            }
            return selectedFilePath;
        }
        public static string GetFolder(string title = "フォルダーを選択してください",
                        string initFolder = @"C:\")
        {
            string selectedFolderPath = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            // ダイアログのタイトルを設定
            folderBrowserDialog.Description = title;
            // 初期フォルダーを設定（オプション）
            folderBrowserDialog.SelectedPath = initFolder;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFolderPath = folderBrowserDialog.SelectedPath;
            }
            return selectedFolderPath;
        }
    }
    public class CONSTANTS
    {
        public static readonly int TIME4DNS = 999998;
        public static readonly int TIME4DNSM = 999997;
        public static readonly int TIME4DQ = 999999;
        public static readonly int DNS = 1;
        public static readonly int DQ = 2;
        public static readonly int DNSM = 3;
        public static readonly string[] reason = new string[] { "", "棄権", "失格", "途中退水","オープン",
            "OP(失格)","OP(棄権)" };
    }
}
