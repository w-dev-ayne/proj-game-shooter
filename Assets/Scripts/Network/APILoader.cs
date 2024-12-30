using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class APILoader
{
    protected async Task<GetData<T>> GetAPI<T>(string endPoint, object data = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{Managers.Network.host}{endPoint}"))
        {
            request.SetRequestHeader("Authorization", "Bearer " + Managers.Network.token);
            
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                Debug.Log($"Response : {json}");
            }
            else
            {
                Debug.LogError(request.error);
            }
            
            GetData<T> result = JsonUtility.FromJson<GetData<T>>(request.downloadHandler.text);

            return result;
        }
    }
    
    /// <summary>
    /// PostAPI 호출 함수
    /// </summary>
    /// <param name="endPoint">API Endpoint</param>
    /// <param name="data">Post시 Body에 포함될 Data, PostData를 상속받은 Class</param>
    /// <typeparam name="T">Response의 Data로 Return 받을 (Serializable)클래스 (자료형)</typeparam>
    /// <returns></returns>
    protected async Task<GetData<T>> PostAPI<T>(string endPoint, PostData data = null, bool isNeedToken = true)
    {
        // 데이터 JSON으로 변환
        string json = JsonUtility.ToJson(data);
        string sanitizedJson = Regex.Replace(json, @"\u200B|\u200C|\u200D", ""); // 필요에 따라 특수 문자를 추가
        byte[] jsonToSend = Encoding.UTF8.GetBytes(sanitizedJson);
        
        // UnityWebRequest 생성
        using (UnityWebRequest request = new UnityWebRequest($"{Managers.Network.host}{endPoint}", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            if(isNeedToken)
                request.SetRequestHeader("Authorization", "Bearer " + Managers.Network.token);

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

            GetData<T> result = JsonUtility.FromJson<GetData<T>>(request.downloadHandler.text);

            return result;
        }
    }
    
    /// <summary>
    /// Editor용 PostAPI 호출 함수
    /// </summary>
    /// <param name="endPoint">API Endpoint</param>
    /// <param name="data">Post시 Body에 포함될 Data, PostData를 상속받은 Class</param>
    /// <typeparam name="T">Response의 Data로 Return 받을 (Serializable)클래스 (자료형)</typeparam>
    /// <returns></returns>
    protected async Task<GetData<T>> EditorPostAPI<T>(string endPoint, NetworkConfig config, PostData data = null, bool isNeedToken = true)
    {
        string url = $"http://{config.host}:{config.port}{endPoint}";
        
        // 데이터 JSON으로 변환
        string json = JsonUtility.ToJson(data);
        string sanitizedJson = Regex.Replace(json, @"\u200B|\u200C|\u200D", ""); // 필요에 따라 특수 문자를 추가
        byte[] jsonToSend = Encoding.UTF8.GetBytes(sanitizedJson);

        Debug.Log($"{url}");
        
        // UnityWebRequest 생성
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            if(isNeedToken)
                request.SetRequestHeader("Authorization", "Bearer " + config.token);

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

            GetData<T> result = JsonUtility.FromJson<GetData<T>>(request.downloadHandler.text);

            return result;
        }
    }
}