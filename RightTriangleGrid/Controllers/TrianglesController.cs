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
        private static string invalidIdMessage = "Invalid ID. Must be a letter followed by a positive integer.";

        [Route("api/triangles/{id}")]
        public IHttpActionResult Get(string id)
        {
            if (id.Length < 2)
            {
                return BadRequest(invalidIdMessage);
            }

            char row = id[0];
            int col;
            bool colParseSuccess = int.TryParse(id.Substring(1), out col);
            if (!colParseSuccess)
            {
                return BadRequest(invalidIdMessage);
            }

            return Get(row, col);
        }

        [Route("api/triangles/row/{row}/column/{column}")]
        public IHttpActionResult Get(char row, int column)
        {
            // Verify the row is a letter and the column is positive
            if (row < 65 || (row > 90 && row < 97) || row > 122 || column <= 0)
            {
                return BadRequest(invalidIdMessage);
            }

            Triangle triangle;
            try
            {
                triangle = new Triangle(row, column);
                triangle.FindVerticesFromId();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(triangle);
        }

        [Route("api/triangles/{v1}/{v2}/{v3}")]
        public IHttpActionResult Get(string v1, string v2, string v3)
        {
            Triangle triangle;
            try
            {
                triangle = new Triangle(
                    new Vertex(v1),
                    new Vertex(v2),
                    new Vertex(v3));

                triangle.FindIdFromVertices();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(triangle);
        }
    }
}