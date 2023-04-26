using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateStacks : MonoBehaviour
{
   private RootObject _rootObject;
   private List<User> _rootObjectsListGrade6;
   private List<User> _rootObjectsListGrade7;
   private List<User> _rootObjectsListGrade8;
   public GameObject blockPrefab;
   public GameObject[] stacksGO;
   private int rowIndex;
   private float xDist = .33f;
   private float yDist = .2f;
   private List<string> labelTextList;
   public static Action<CreateStacks> OnStacksCreationFinished;
   public Material[] blockMaterials;
   private List<GameObject> glassBlocks=new List<GameObject>();
   public void SetRootObject(RootObject rootObject)
   {
      _rootObject = rootObject;
      labelTextList = new List<string>();
      DivideByGrades();
      _rootObjectsListGrade6=SortList(_rootObjectsListGrade6);
      _rootObjectsListGrade7=SortList(_rootObjectsListGrade7); 
      _rootObjectsListGrade8=SortList(_rootObjectsListGrade8); 

      CreateStacksBasedOnGrades();
      
   }

   void DivideByGrades()
   {
      _rootObjectsListGrade6 = new List<User>();
      _rootObjectsListGrade7 = new List<User>();
      _rootObjectsListGrade8 = new List<User>();

      for (int i = 0; i < _rootObject.users.Length; i++)
      {
         if(_rootObject.users[i].grade=="6th Grade")
            _rootObjectsListGrade6.Add(_rootObject.users[i]);
         else if(_rootObject.users[i].grade=="7th Grade")
            _rootObjectsListGrade7.Add(_rootObject.users[i]);
         else if(_rootObject.users[i].grade=="8th Grade")
            _rootObjectsListGrade8.Add(_rootObject.users[i]);
      }
     

   }

   List<User> SortList(List<User> GradeListToSort)
   {
      List<User> SortedListByDomainName = new List<User>();
      SortedListByDomainName= SortByDomainName(GradeListToSort);
      
      List<User> SortedListByClusterName = new List<User>();
      SortedListByClusterName= SortByClusterName(SortedListByDomainName);
      List<User> SortedListByStandardId = new List<User>();

      SortedListByStandardId = SortByStandardId(SortedListByClusterName);
      
      GradeListToSort.Clear();
      return SortedListByStandardId;
      
    
   }

   List<User> SortByDomainName(List<User> GradeListToSort)
   {
      List<User> sortedListByDomainName = new List<User>();
   
      for (int i = 0; i < GradeListToSort.Count; i++)
      {  
             sortedListByDomainName = GradeListToSort.OrderBy(go=>go.domain).ToList();
      }

      return sortedListByDomainName; 
   }

   List<User>  SortByClusterName(List<User> listToSort)
   {  
      List<User> sortedListByClusterName = new List<User>();

      for (int i = 0; i < listToSort.Count; i++)
      {  
         sortedListByClusterName = listToSort.OrderBy(go=>go.cluster).ToList();
      }
      return sortedListByClusterName;

   }

   List<User>  SortByStandardId(List<User> listToSort)
   {  
      List<User> sortedListByStandardId = new List<User>();

      for (int i = 0; i < listToSort.Count; i++)
      {  
         sortedListByStandardId = listToSort.OrderBy(go=>go.standardid).ToList();
      }
      return sortedListByStandardId;

   }
   
   void CreateStacksBasedOnGrades()
   {
      
      CreateStack(_rootObjectsListGrade6, stacksGO[0],0);
      CreateStack(_rootObjectsListGrade7, stacksGO[1],1);
      CreateStack(_rootObjectsListGrade8, stacksGO[2],2); 
      
   }

   void CreateStack(List<User> GradeList, GameObject stack,int stackIndex)
   { 
     List<GameObject> blocksList = new List<GameObject>();
     List<GameObject>  rowsList = new List<GameObject>();

      blocksList.Clear();
      rowsList.Clear(); 
      bool gotLabelText=false;
      rowIndex = 0;
      //spawning blocks
      for (int i = 0; i < GradeList.Count; i++)
      {
         Vector3 pos= Vector3.zero;
         int div = i / 3; 
         pos.x = i%3*xDist; 
         pos.y = div*yDist;

         GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity,stack.transform); 
         blocksList.Add(block);
         if (div == rowIndex)
         {
            rowIndex++;
            GameObject row;
            row = new GameObject(rowIndex.ToString());
            rowsList.Add(row);
         }
         Block blockScript = block.GetComponent<Block>();
         int materialIndex =  GradeList[i].mastery;
         block.GetComponent<Renderer>().material = blockMaterials[materialIndex];
         blockScript.BlockType = (BlockType) GradeList[i].mastery;
         
         blockScript.id = GradeList[i].id;
         blockScript.subject = GradeList[i].subject;
         blockScript.grade = GradeList[i].grade;
         blockScript.domainid = GradeList[i].domainid;
         blockScript.mastery = GradeList[i].mastery;
         blockScript.domain = GradeList[i].domain;
         blockScript.cluster = GradeList[i].cluster;
         blockScript.standardid = GradeList[i].standardid;
         blockScript.standarddescription = GradeList[i].standarddescription;

         if( blockScript.BlockType==BlockType.glass)
            glassBlocks.Add(block);
         
         if (!gotLabelText)
         {
            labelTextList.Add(GradeList[i].grade);
            gotLabelText = true;
         }
      }
 
      
      //assigning row parents
      for (int i = 0; i < blocksList.Count; i++)
      {
         int div = i / 3;
          blocksList[i].transform.SetParent(rowsList[div].transform);
      }
      
      //rotate row parents that are even numbers
      for (int i = 0; i < rowsList.Count; i++)
      {
         if (int.Parse(rowsList[i].name) % 2 == 1)
         {
            rowsList[i].transform.Rotate( 0 , 90 , 0);
            rowsList[i].transform.position = new Vector3(xDist, 0, xDist);
         }
         rowsList[i].transform.SetParent(stack.transform); 
      } 
      stack.transform.position = new Vector3(3*(stackIndex-1), 0, 0);
      
      if(stackIndex==2)
         OnStacksCreationFinished.Invoke(this); 
   }

   public Vector3 GetGradeLabelPosition(int index)
   {
      Vector3 pos = stacksGO[index].transform.position;
      pos.z -= 1;
      pos.y = .3f;
      return pos;
   }

   public string GetGradeLabelText(int index)
   {
      return labelTextList[index];
   }

   public List<GameObject> GetGlassBlocks()
   {
      return glassBlocks;
   }
}
