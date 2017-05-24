using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//##################################################
// Author: Paulo Costa
// Last Revision: 24/05/2017
//##################################################

public partial class xml_todo : System.Web.UI.Page
{


    private string strXML = "";
    ToDoBR lstTodo = new ToDoBR();




    //-----------------------------------------------------------------------------
    // The page at loading expects to find the parameter action at Request["action"] to execute one operation
    // Parameters:
    //      Request["action"] -> Type: string
    //
    //    Request["action"] might use the following values as input parameters:
    //      add - adds a new element                -> (this function needs more parameters, see function details)
    //      list - lists all elements
    //      complete - marks a task as completed    -> (this function needs more parameters, see function details)
    //      edit - edit one particular task         -> (this function needs more parameters, see function details)
    //      remove - remove one particular task     -> (this function needs more parameters, see function details)

    // Exceptions: in case of error/exception a single node with the error message will be returned 
    //  within the tag <error_message>

    protected void Page_Load(object sender, EventArgs e)
    {
        
        //clear headers and define the response will be XML type
        Response.ClearHeaders();
        Response.AddHeader("content-type", "text/xml");

        //start the XML and executes the appropriate method
        try
        {
            strXML += "<ToDoList>";
            if (Request["action"] != null)
            {
                switch (Request["action"].Trim())
                {
                    case "add":
                        Add();
                        List();
                        break;
                    case "list":
                        List();
                        break;
                    case "edit":
                        Edit();
                        List();
                        break;
                    case "complete":
                        Complete();
                        List();
                        break;
                    case "remove":
                        Remove();
                        List();
                        break;
                }
            }
            strXML += "</ToDoList>";
        }
        catch (Exception ex)
        {
            //in case of error the XML will return this tag only with the error message
            strXML = "<error_message>" + ex.Message + "</error_message>";
        }


        strXML = "<?xml version='1.0' encoding='iso-8859-1'?>" + strXML;

        Response.Write(strXML);
    }


    //#####################################################################################
    //#####################################################################################



    //-----------------------------------------------------------------------------
    /// <summary>
    /// Adds a new element
    /// Parameters:
    ///      Request["completed"]    -> Type: int
    ///      Request["header"]       -> Type: string
    ///      Request["body"]         -> Type: string
    /// </summary>
    private void Add()
    {
        int completed = 0;
        string header = "";
        string body = "";

        if (Request["completed"] != null) { completed = int.Parse(Request["completed"]);  }
        if (Request["header"] != null) { header=Request["header"]; }
        if (Request["body"] != null) { body = Request["body"]; }

        lstTodo.Add(completed, header, body);

    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Lists all elements in the list
    /// Parameters: none
    /// </summary>
    private void List()
    {
        List<ToDo.ToDoVC> printLstTodo = lstTodo.GetTasks();
        int i = 0; //index counter

        foreach (ToDo.ToDoVC item in printLstTodo)
        {
            strXML += "<ToDo>";
            strXML += "<index>" + i.ToString() + "</index>";
            strXML += "<complete>" + item.completed.ToString() + "</complete>";
            strXML += "<header>" + item.header + "</header>";
            strXML += "<body>" + item.body + "</body>";
            strXML += "</ToDo>";

            i++;
        }

    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Edit one particular task
    /// Parameters:
    ///      Request["index"]    -> Type: int
    ///      Request["complete"] -> Type: int
    ///      Request["header"]   -> Type: string
    ///      Request["body"]     -> Type: string
    /// </summary>
    private void Edit()
    {
        int index = 0;
        int completed = 0;
        string header = "";
        string body = "";

        if (Request["index"] == null) { return; }
        else { index = int.Parse(Request["index"]); }

        if (Request["complete"] != null) { completed = int.Parse(Request["complete"]); }
        if (Request["header"] != null) { header = Request["header"]; }
        if (Request["body"] != null) { body = Request["body"]; }
        
        lstTodo.Edit(index , completed, header, body);

        
    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Marks a task as completed
    /// Parameters:
    ///      Request["index"]    -> Type: int
    ///      Request["complete"] -> Type: int
    /// </summary>
    private void Complete()
    {
        int index = 0;
        int completed = 0;
        if (Request["index"] == null) { return; }
        else { index = int.Parse(Request["index"]); }

        if (Request["complete"] != null) { completed = int.Parse(Request["complete"]); }

        lstTodo.Complete(index, completed);

    }

    //-----------------------------------------------------------------------------
    /// <summary>
    /// Remove one particular task
    /// Parameters: 
    ///      Request["index"]    -> Type: int
    /// </summary>
    private void Remove()
    {
        int index = 0;
        if (Request["index"] == null) { return; }
        else { index = int.Parse(Request["index"]); }

        lstTodo.Remove(index);

    }
}
