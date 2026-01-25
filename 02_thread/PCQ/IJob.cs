using System;
using System.Collections.Generic;
using System.Text;

namespace PCQ;

public interface IJob
{
    public void Execute();
    public string GetInfo();        // For debug only!
}
