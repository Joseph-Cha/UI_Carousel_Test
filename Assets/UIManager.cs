using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Direction
{
    right,
    left
}

public class UIManager : MonoBehaviour
{
    private int curIndex;
    private int totalIndex;

    private float uiMoveTime = 0.3f;
    private float moveValue = 1100;

    public GameObject[] ImagePrefabs;
    public Transform ParentPos; 

    // ImagePrefabs의 저장 공간
    public GameObject LeftRoom;
    public GameObject CenterRoom;
    public GameObject RightRoom;

    // 룸의 RectTransform 저장 공간
    private RectTransform LeftRectPos;
    private RectTransform CenterRectPos;
    private RectTransform RightRectPos;

    // 단순 위치 저장용
    public RectTransform LeftRoomPos;
    public RectTransform CenterRoomPos;
    public RectTransform RightRoomPos;

    private void Start()
    {
        curIndex = 0;
        totalIndex = ImagePrefabs.Length;
    }

    public void ClickNextBtn()
    {

        if (curIndex == 0)
        {
            curIndex++;
            PlayContent(Direction.right, 2);
            Debug.Log("index: " + curIndex);
        }

        else if (curIndex == 1)
        {
            curIndex++;
            PlayContent(Direction.right, 1);
            Debug.Log("index: " + curIndex);
        }

        else if (curIndex == 2)
        {
            curIndex++;
            PlayContent(Direction.right, 0);
            Debug.Log("index: " + curIndex);
        }

        else if (curIndex == 3)
        {
            curIndex = 0;
            PlayContent(Direction.right, 3);
            Debug.Log("index: " + curIndex);
        }
    }

    public void ClickPrevBtn()
    {

        if (curIndex == 0)
        {
            curIndex = 3;
            PlayContent(Direction.left, 2);
            Debug.Log("index: " + curIndex);
        }
        else if (curIndex == 3)
        {
            curIndex--;
            PlayContent(Direction.left, 3);
            Debug.Log("index: " + curIndex);
        }
        else if (curIndex == 2)
        {
            curIndex--;
            PlayContent(Direction.left, 0);
            Debug.Log("index: " + curIndex);
        }
        else if (curIndex == 1)
        {
            curIndex--;
            PlayContent(Direction.left, 1);
            Debug.Log("index: " + curIndex);
        }
    }

    public void PlayContent(Direction _direction, int _index)
    {
        switch (_direction)
        {
            case Direction.right:
                Destroy(_direction);
                UIAnimation(_direction);
                Create(_direction, _index);
                break;

            case Direction.left:
                Destroy(_direction);
                UIAnimation(_direction);
                Create(_direction, _index);
                break;
        }
    }

    private void Destroy(Direction _direction)
    {
        switch (_direction)
        {
            // 오른쪽 버튼을 클릭하면 오른쪽 방을 비우고 채워 넣을 준비
            case Direction.right:
                Destroy(RightRoom.gameObject);
                break;

            // 왼쪽 버튼을 클릭하면 왼쪽 방을 비우고 채워 넣을 준비
            case Direction.left:
                Destroy(LeftRoom.gameObject);
                break;
        }
    }

    private void UIAnimation(Direction _direction)
    {
        switch (_direction)
        {
            // RightRoom <- CenterRoom 이동
            // RightRoom <- LeftRoom 이동
            // Tweening.DOAnchorPos : UI 이동 애니메이션 Lib
            case Direction.right:
                CenterRectPos = CenterRoom.GetComponent<RectTransform>();
                LeftRectPos = LeftRoom.GetComponent<RectTransform>();
                CenterRectPos.DOAnchorPos(CenterRectPos.anchoredPosition + new Vector2(moveValue, 0), uiMoveTime);
                LeftRectPos.DOAnchorPos(LeftRectPos.anchoredPosition + new Vector2(moveValue, 0), uiMoveTime);
                RightRoom = CenterRoom;
                CenterRoom = LeftRoom;
                break;

            // LeftRoom <- CenterRoom  이동
            // CenterRoom <- RightRoom 이동
            // Tweening.DOAnchorPos : UI 이동 애니메이션 Lib
            case Direction.left:
                CenterRectPos = CenterRoom.GetComponent<RectTransform>();
                RightRectPos = RightRoom.GetComponent<RectTransform>();
                CenterRectPos.DOAnchorPos(CenterRectPos.anchoredPosition + new Vector2(-moveValue, 0), uiMoveTime);
                RightRectPos.DOAnchorPos(RightRectPos.anchoredPosition + new Vector2(-moveValue, 0), uiMoveTime);
                LeftRoom = CenterRoom;
                CenterRoom = RightRoom;
                break;
        }
    }

    private void Create(Direction direction, int _index)
    {
        switch (direction)
        {
            // LeftRoom에 오브젝트 생성
            case Direction.right:
                LeftRoom = Instantiate(ImagePrefabs[_index], new Vector2(0, 0), Quaternion.identity);
                LeftRoom.transform.parent = ParentPos;
                LeftRoom.transform.localPosition = new Vector2(-moveValue, 0);
                break;

            // RightRoom에 오브젝트 생성
            case Direction.left:
                RightRoom = Instantiate(ImagePrefabs[_index], new Vector2(0, 0), Quaternion.identity);
                RightRoom.transform.parent = ParentPos;
                RightRoom.transform.localPosition = new Vector2(moveValue, 0);
                break;
        }
    }
}
