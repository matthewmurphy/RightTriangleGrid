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
        private const string invalidIdMessage = "Invalid ID. Must be a letter followed by a positive integer.";

        Triangle[] triangles = new Triangle[]
        {
        };

        public IEnumerable<Triangle> GetAllTriangles()
        {
            return triangles;
        }

        public IHttpActionResult GetTriangle(string id)
        {
            if (id.Length != 2)
            {
                return BadRequest(invalidIdMessage);
            }

            char row = id[0];
            int col;
            bool colParseSuccess = int.TryParse(id[1].ToString(), out col);
            if (!colParseSuccess)
            {
                return BadRequest(invalidIdMessage);
            }

            return Get(row, col);
        }

        [Route("api/triangles/{row}/{column}")]
        public IHttpActionResult Get(char row, int column)
        {
            // Verify the row is a letter
            if (row < 65 || (row > 90 && row < 97) || row > 122)
                return BadRequest(invalidIdMessage);

            if (column <= 0)
                return BadRequest(invalidIdMessage);

            var triangle = new Triangle(row, column);
            triangle.FindVerticesFromId();

            return Ok(triangle);
        }

        [Route("api/triangles/{v1x}/{v1y}/{v2x}/{v2y}/{v3x}/{v3y}")]
        public IHttpActionResult Get(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            var triangle = new Triangle(
                new Vertex(v1x, v1y),
                new Vertex(v2x, v2y),
                new Vertex(v3x, v3y));
            try
            {
                triangle.FindIdFromVertices();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(triangle);
        }
    }
}