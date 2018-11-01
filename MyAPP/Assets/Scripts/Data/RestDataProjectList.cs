using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RestDataProjectList
{
    public int total;
    public RestDataProject[] rows;

    public RestDataProjectList()
    {
        this.total = 0;
        rows = new RestDataProject[0];
    }
}
