using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using System.Text.RegularExpressions;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public List<string> Check()
        {
            var Input = Request.Form["input"][0];
            var Rows = Input.Split("\n");
            Dictionary<string, string> Subjects = new Dictionary<string, string> { };
            List<string> Courses = new List<string> { };
            List<string> Temp = new List<string> { };
            for(var i = 0; i < Rows.Length; i++)
            {
                var tmp = Rows[i].Split(": ");
                if (tmp.Length > 1)
                {
                    Subjects.Add(tmp[0], tmp[1]);
                    if(tmp[1]=="")
                        Courses.Add(tmp[0]);
                }
                else
                {
                    Subjects.Add(tmp[0], null);
                    Courses.Add(tmp[0]);
                }
            }

            foreach (KeyValuePair<string, string> main in Subjects)
            {
                Temp = getList(Subjects, main, Temp);
                if(Temp == null)
                {
                    return null;
                }
                else
                {
                    Temp.Reverse();
                    foreach (var i in Temp)
                    {
                        if (!Courses.Contains(i))
                            Courses.Add(i);
                    }
                    if (!Courses.Contains(main.Key) && main.Key != null && main.Key != "")
                        Courses.Add(main.Key);
                    if (!Courses.Contains(main.Value) && main.Value != null && main.Value != "")
                        Courses.Add(main.Value);
                }
            }
            return Courses;
        }

        private List<string> getList(Dictionary<string, string> Subjects, KeyValuePair<string, string> main, List<string> result)
        {
            if(result.Count > Subjects.Count*2)
            {
                return null;
            }
            foreach(KeyValuePair<string, string> sub in Subjects)
            {
                if(main.Value == sub.Key && sub.Value != "")
                {
                    result.Add(sub.Value);
                    getList(Subjects, sub, result);
                }
            }
            return result;
        }
    }
}
