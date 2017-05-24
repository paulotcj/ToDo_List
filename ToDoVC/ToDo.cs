using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//##################################################
// Value Class for the ToDo list
//
// Author: Paulo Costa
// Last Revision: 23/05/2017
//##################################################
 

namespace ToDo
{
    /// <summary>
    /// ToDoVC: Value Class for the ToDo list
    /// </summary>
    public class ToDoVC : ICloneable
    {
        int _completed = 0;
        string _header = "";
        string _body = "";


        //-----------------------------------------------------------------------------
        /// <summary>
        /// Marks if the task is completed.
        /// </summary>
        public int completed
        {
            get { return _completed; }
            set { _completed = value; }
        }



        //-----------------------------------------------------------------------------
        /// <summary>
        /// Header of the Task.
        /// </summary>
        public string header
        {
            get { return _header; }
            set { _header = value; }
        }


        //-----------------------------------------------------------------------------
        /// <summary>
        /// Body of the Task.
        /// </summary>
        public string body
        {
            get { return _body; }
            set { _body = value; }
        }


        //-----------------------------------------------------------------------------
        /// <summary>
        /// Copy this object.
        /// </summary>
        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
