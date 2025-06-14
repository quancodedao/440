using System;
using System.Collections;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private string pooledObjectId;
    //    objectPool: Tham chiếu đến một object pool(bể chứa đối tượng), nơi quản lý việc tái sử dụng các GameObject để tránh việc khởi tạo/huỷ quá nhiều lần.
    //pooledObjectId: ID của đối tượng sẽ được lấy từ pool.
    private float currentX = 0;
    //currentX: Tọa độ X hiện tại, để các object được spawn không bị chồng lên nhau, mà dịch sang phải mỗi lần 1 đơn vị.
    public void Spawn(string pooledObjectId)
    {
        var go = objectPool.GetObject(pooledObjectId);
        //Gọi hàm GetObject() từ pool để lấy ra một object theo ID.
        if (go != null)
        {
            go.transform.position = new Vector3(currentX, 0, 0);
            go.transform.rotation = Quaternion.identity;
            currentX++;
            //Invoke("ReturnObject",go, 1f);
            StartCoroutine(ReturnObject(go));
            //Nếu lấy được object:
            //Đặt vị trí mới theo currentX, trục Y và Z là 0.
            //Reset lại rotation về mặc định.
            //Tăng currentX để lần sau spawn ở vị trí khác.
            //Gọi coroutine ReturnObject(go) để đợi 1 giây rồi trả object về pool.
        }
        else
        {
            Debug.LogWarning($"No object found with ID: {pooledObjectId}");
            //Nếu không lấy được object(null), hiển thị cảnh báo.
        }
    }

    private IEnumerator ReturnObject(GameObject go)
    {
        yield return new WaitForSeconds(1f);
        //Đợi 1 giây.
        objectPool.ReturnObject(pooledObjectId, go);
        currentX = 0;
        //        Trả object về pool.
        //Reset lại currentX để lần spawn tiếp theo bắt đầu lại từ 0.
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Object"))
        {
            Spawn(pooledObjectId);
        }
        //        Tạo một nút trên màn hình(UI dạng đơn giản của Unity).

        //Khi người dùng nhấn nút, hàm Spawn() được gọi.
    }
}