using RightTriangleGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace RightTriangleGrid.Controllers
{
    public class RightTriangleGridController : ApiController
    {
        Triangle[] triangles = new Triangle[]
        {
            new Triangle
            {
                Label = new TriangleLabel { Row = 'A', Column = 1 },
                A = new Point(0,0),
                B = new Point(0,10),
                C = new Point (10,10)
            },
            new Triangle
            {
                Label = new TriangleLabel { Row = 'A', Column = 2 },
                A = new Point(0,0),
                B = new Point(10,0),
                C = new Point (10,10)
            }
        };

        public IEnumerable<Triangle> GetAllProducts()
        {
            return triangles;
        }

        public IHttpActionResult GetProduct(char row, int column)
        {
            var triangle = triangles.SingleOrDefault(t => t.Label.Row == row && t.Label.Column == column);
            if (triangle == null)
            {
                return NotFound();
            }
            return Ok(triangle);
        }
    }
}