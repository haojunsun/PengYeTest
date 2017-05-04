using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PengYe.Project.Infrastructure
{
    /// <summary>
    /// 依赖注入框架，继承于此框架的Interface具备依赖注入条件。
    /// </summary>
    public interface IDependency
    {
    }

    public interface IDependencyPerRequest
    {
    }
}
