 
using System;
using UnityEngine;

public class BlockTouch : MonoBehaviour
{
   private Vector3 mOffset;
   private float mZCoord;
   [SerializeField]private Block _block;
   void OnMouseDown()
   {
      mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; 
      mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
   }

  

   private Vector3 GetMouseAsWorldPoint()
   { 
      Vector3 mousePoint = Input.mousePosition; 
      mousePoint.z = mZCoord; 
      return Camera.main.ScreenToWorldPoint(mousePoint);
   }
   
   void OnMouseDrag()
   {
      transform.position = GetMouseAsWorldPoint() + mOffset;
   }

   private void OnMouseOver()
   {
      if (Input.GetMouseButton(1))
      {
         BlockInfo.instance.SetBlockInfoText(_block);



      }
   }
}
