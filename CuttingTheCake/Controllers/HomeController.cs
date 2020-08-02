using CuttingTheCake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuttingTheCake.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData(DataModel model)
        {
            try
            {
                var ListA = model.AElements.Split('.').ToList().ConvertAll(int.Parse);
                var ListB = model.BElements.Split('.').ToList().ConvertAll(int.Parse);
                int data = Solution(model.Height, model.Width, model.Kth, ListA, ListB, model.NOP);
                return Json(new { isSuccess = true, result = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false }, JsonRequestBehavior.AllowGet);
            }

        }

        public int Solution(int X, int Y, int K, List<int> A, List<int> B, int N)
        {
            int[] Width = new int[A.Count + 1];
            int[] Height = new int[B.Count + 1];
            int[] result = new int[Convert.ToInt32(Math.Pow(N + 1, 2))];

            int counterA = 0;
            for (int indexA = 0; indexA < A.Count + 1; indexA++)
            {
                //Fill the Width Array
                if (indexA == 0)
                {
                    Width[indexA] = A[indexA];
                }
                else if (indexA == A.Count)
                {
                    Width[indexA] = X - Width.Sum();
                }
                else
                {
                    counterA++;
                    Width[indexA] = A[indexA] - Width[indexA - counterA];
                }

            }
            int counterB = 0;

            for (int indexB = 0; indexB < B.Count() + 1; indexB++)
            {
                //Fill the Width Array
                if (indexB == 0)
                {
                    Height[indexB] = B[indexB];
                }
                else if (indexB == B.Count)
                {
                    Height[indexB] = Y - Height.Sum();
                }
                else
                {
                    counterB++;
                    Height[indexB] = B[indexB] - Height[indexB - counterB];
                }
            }

            int resindex = 0;
            for (int i = 0; i < Height.Count(); i++)
            {
                for (int j = 0; j < Width.Count(); j++)
                {
                    result[resindex] = Height[i] * Width[j];
                    resindex++;
                }
            }
            Array.Sort<int>(result, new Comparison<int>((i1, i2) => i2.CompareTo(i1)));
            return result[K - 1];
        }
    }
}