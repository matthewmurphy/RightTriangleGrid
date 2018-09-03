using RightTriangleGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace RightTriangleGrid.Controllers
{
    public class TrianglesController : ApiController
    {
        Triangle[] triangles = new Triangle[]
        {
            new Triangle
            {
                ID = "A1",
                P1 = new Point(0,0),
                P2 = new Point(0,10),
                P3 = new Point (10,10)
            },
            new Triangle
            {
                ID = "A2",
                P1 = new Point(0,0),
                P2 = new Point(10,0),
                P3 = new Point (10,10)
            },
            new Triangle
            {
                ID = "B1",
                P1 = new Point(0,10),
                P2 = new Point(10,20),
                P3 = new Point (0,20)
            }
        };

        public IEnumerable<Triangle> GetAllTriangles()
        {
            return triangles;
        }

        public IHttpActionResult GetTriangle(string id)
        {
            if (id.Length != 2)
            {
                return NotFound();
            }

            char row = id[0];
            int col;
            bool colParseSuccess = int.TryParse(id[1].ToString(), out col);
            if (!colParseSuccess)
            {
                return NotFound();
            }

            var triangle = triangles.FirstOrDefault(t => t.ID.ToString().ToUpper() == id.ToUpper());
            if (triangle == null)
            {
                return NotFound();
            }
            return Ok(triangle);
        }
    }
}