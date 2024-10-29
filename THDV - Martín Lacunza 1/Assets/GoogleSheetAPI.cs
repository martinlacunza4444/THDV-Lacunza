using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Google.Apis.Services;
using System;
using Google.Apis.Sheets.v4.Data;
using System.IO;
using System.Threading.Tasks;

public class GoogleSheetsForUnity : MonoBehaviour
{
    [Header("GoogleSheets Information")]
    [SerializeField] private string spreadSheetID;
    [SerializeField] private string sheetID;

    [Header("Data from GoogleSheets")]
    [SerializeField] private string getDataInRange;

    private string serviceAccountEmail = "googlesheetunity@unityapi-440013.iam.gserviceaccount.com";
    private string certificateName = "unityapi-440013-fb79c36c622b.p12";
    private string certificatePath;

    private static SheetsService googleSheetsService;
    [Serializable]
    public class Row
    {
        public List<string> cellData = new List<string>();
    }
    [Serializable]
    public class RowList
    {
        public List<Row> rows = new List<Row>();
    }

    public RowList DataFromGoogleSheets = new RowList();

    [Header("Write Data From Unity")]
    [SerializeField] private string writeDataInRange;

    public RowList WriteDataFromUnity = new RowList();

    [Header("Delete Data In GoogleSheets")]
    [SerializeField] private string deleteDataInRange;


    void Start()
    {
        


        certificatePath = Application.dataPath + "/StreamingAssets/" + certificateName;  //Comment to use on Android

        var certificate = new X509Certificate2(certificatePath, "notasecret", X509KeyStorageFlags.Exportable);

        ServiceAccountCredential credential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = new[] { SheetsService.Scope.Spreadsheets }
            }.FromCertificate(certificate));

        googleSheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "GoogleSheets API for Unity"
        });
        ReadData();
    }

    public void ReadData()
    {
        string range = sheetID + "!" + getDataInRange;

        var request = googleSheetsService.Spreadsheets.Values.Get(spreadSheetID, range);
        var reponse = request.Execute();
        var values = reponse.Values;
        if (values != null && values.Count > 0)
        {
            foreach (var row in values)
            {
                Row newRow = new Row();
                DataFromGoogleSheets.rows.Add(newRow);
                foreach (var value in row)
                {
                    newRow.cellData.Add(value.ToString());
                    Debug.Log(value.ToString());
                }

            }
        }
    }

    public async void ReadDataAsyn()
    {
        var task = await Task.Run(() =>
        {
            string range = sheetID + "!" + getDataInRange;

            var request = googleSheetsService.Spreadsheets.Values.Get(spreadSheetID, range);
            var reponse = request.Execute();
            var values = reponse.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Row newRow = new Row();
                    DataFromGoogleSheets.rows.Add(newRow);
                    foreach (var value in row)
                    {
                        newRow.cellData.Add(value.ToString());
                    }

                }
            }
            return 0;
        });
    }
}