using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	//引用 EmptySquare prefab
	//这里没有用 GameObject 主要是避免写太长的链式属性才访问需要的值
	//所以直接引用 Transform 属性就够了
	public Transform emptySprite;

	public int height = 30;
	public int width = 10;

	public int header = 8;

	Transform [,] grid;

	void Awake(){
		grid = new Transform[width, height];
	}

	void Start () {
		DrawEmptyCells ();
	}



	bool IsWithinBoard(int x, int y){
		return (x >= 0 && x < width && y >= 0);
	}

	bool IsOccupied(int x, int y, Shape shape){
		return (grid [x, y] != null) && (grid [x, y].parent != shape.transform);
	}

	public bool IsValidPosition(Shape shape){
		foreach (Transform child in shape.transform) {
			Vector2 pos = Vectorf.Round (child.position);
			if (!IsWithinBoard((int) pos.x, (int) pos.y )){
				return false;
			}

			if(IsOccupied((int) pos.x, (int) pos.y, shape)){
				return false;
			}
		}
		return true;
	}

	void DrawEmptyCells(){
		for (int y = 0; y < height - header; y++) {
			for (int x = 0; x < width; x++) {
				Transform clone;
				clone = Instantiate (emptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
				clone.name = "Board Space ( x = " + x.ToString() + ", y = " + y.ToString() + " )";
				clone.transform.parent = transform; //把克隆对象赋给源 prefab
			}
		}
	}

	public void StoreShapeInGrid(Shape shape){
		if (shape == null)
			return;

		foreach (Transform child in shape.transform) {
			Vector2 pos = Vectorf.Round (child.position);
			grid [(int)pos.x, (int)pos.y] = child;
		}
	}

	bool IsComplete(int y){
		for (int x = 0; x < width; ++x) {
			if (grid [x, y] == null) {
				return false;
			}
		}
		return true;
	}

	void ClearRow(int y){
		for (int x = 0; x < width; ++x){
			if(grid[x, y] != null){
				Destroy(grid[x, y].gameObject);
			}
			grid[x,y] = null;
		}
	}

	void ShiftOneRowDown(int y){
		for (int x = 0; x < width; ++x) {
			if(grid[x, y] != null){
				//把 Transform 中的 component 交给新的 IEnumerable
				grid [x, y - 1] = grid [x, y];
				grid [x, y] = null;
				//把 component 的 position 下移
				grid [x, y - 1].position += new Vector3 (0, -1, 0);
			}
		}
	}

	void ShiftRowsDown(int startY){
		for (int i = startY; i < height; ++i){
			ShiftOneRowDown (i);
		}
	}

	public void ClearAllRows(){
		for (int y = 0; y < height; ++y) {
			if (IsComplete (y)) {
				ClearRow (y);
				//把这一行之上的全部下移1个单位
				ShiftRowsDown (y + 1);
				//下移之后这一行要检查是否完成
				y--;
			}
		}
	}

	public bool IsOverLimit(Shape shape){
		foreach (Transform child in shape.transform) {
			if (child.transform.position.y >= (height - header - 1)){
				return true;
			}
		}
		return false;
	}
}
