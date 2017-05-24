using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ToDoBR : Business Rule Class that controls the behaviour of the ToDo list
/// </summary>
 
//##################################################
// Business Rule Class for the ToDo list
//
// Author: Paulo Costa
// Last Revision: 23/05/2017
//##################################################



public class ToDoBR
{

    List<ToDo.ToDoVC> lstTodo;
    
 
    public ToDoBR()
    {
        if (HttpContext.Current.Cache["lstTodo"] != null)
        {
            lstTodo = HttpContext.Current.Cache["lstTodo"] as List<ToDo.ToDoVC>;
        }
        else
        {
            lstTodo = new List<ToDo.ToDoVC>();
        }

    }



    //-----------------------------------------------------------------------------
    /// <summary>
    /// Adds a new item in the ToDo list
    /// </summary>
    public void Add(int completed, string header, string body)
    {

        if (header == "") { throw new System.Exception("header is empty"); }

        ToDo.ToDoVC newObjToDo = new ToDo.ToDoVC();

        newObjToDo.header = header;
        newObjToDo.body = body;
        newObjToDo.completed = 0;


        lstTodo.Add(newObjToDo);

        HttpContext.Current.Cache["lstTodo"] = lstTodo;

    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Removes an item from the ToDo list
    /// </summary>
    public void Remove(int index)
    {
        if (index < 0) { throw new System.Exception("index is less than zero"); }

        lstTodo.RemoveAt(index);

        HttpContext.Current.Cache["lstTodo"] = lstTodo;

    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Edit a item from the ToDo list
    /// </summary>
    public void Edit(int index, int completed, string header, string body)
    {

        if (index < 0) { throw new System.Exception("index is less than zero"); }
        if (header == "") { throw new System.Exception("header is empty"); }


        lstTodo[index].header = header;
        lstTodo[index].body = body;
        lstTodo[index].completed = completed;

        HttpContext.Current.Cache["lstTodo"] = lstTodo;
    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Marks a task as complete or incomplete in the ToDo List. 0 = incomplete, 1 = completed.
    /// </summary>
    public void Complete(int index, int completed)
    {
        Edit(index, completed, lstTodo[index].header, lstTodo[index].body);
    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Return a string with the contents of the task at the index used in the parameter. Fields separated by "|"
    ///   The order is: index, completed, header, body
    /// </summary>
    public string ToString(int index)
    {
        return index.ToString() + "|" + lstTodo[index].completed.ToString() + "|" + lstTodo[index].header + "|" + lstTodo[index].body;
    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Returns one task.
    /// </summary>
    public ToDo.ToDoVC GetTask(int index)
    {
        return (ToDo.ToDoVC)lstTodo[index].Clone();
    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Returns all tasks.
    /// </summary>
    public List<ToDo.ToDoVC> GetTasks()
    {
        List<ToDo.ToDoVC> newLstTodo = new List<ToDo.ToDoVC>();

        foreach (ToDo.ToDoVC item in lstTodo)
        {
            newLstTodo.Add((ToDo.ToDoVC)item.Clone());
        }

        return newLstTodo;

    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Count all the elements in the ToDo list.
    /// </summary>
    public int Count()
    {
        return lstTodo.Count();
    }



}
