using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEditor;

public class APILoader
{
    private string token = null;
    protected async Task GetAPI(string url, object data = null, object overrideData = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", "Bearer " + token);
            
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                JsonUtility.FromJsonOverwrite(json, overrideData);
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
    }

    protected async Task<GetData> PostAPI(string url, PostData data = null)
    {
        // 데이터 JSON으로 변환
        string json = JsonUtility.ToJson(data);
        string sanitizedJson = Regex.Replace(json, @"\u200B|\u200C|\u200D", ""); // 필요에 따라 특수 문자를 추가
        byte[] jsonToSend = Encoding.UTF8.GetBytes(sanitizedJson);

        // UnityWebRequest 생성
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);

            // 요청 전송 및 대기
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }

            GetData result = JsonUtility.FromJson<GetData>(request.downloadHandler.text);

            return result;
        }
    }

    protected void SetToken(string value)
    {
        token = value;
    }
    
}