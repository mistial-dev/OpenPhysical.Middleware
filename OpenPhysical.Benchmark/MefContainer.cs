using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using JetBrains.Annotations;

namespace OpenPhysical.Benchmark;

[PublicAPI]
public class MefContainer
{
    private readonly CompositionContainer _container;

    public MefContainer()
    {
        var assemblies = new List<Assembly>
        {
            Assembly.GetExecutingAssembly(),
            Assembly.Load("OpenPhysical.CardEdge"),
            Assembly.Load("OpenPhysical.Middleware"),
        };

        var catalog = new AggregateCatalog(assemblies.Select(a => new AssemblyCatalog(a)));
        _container = new CompositionContainer(catalog);

        _container.ComposeParts(this);
    }

    public IEnumerable<T> GetExportedValues<T>()
    {
        return _container.GetExportedValues<T>();
    }

    public void ComposeParts(object attributedPart)
    {
        _container.ComposeParts(attributedPart);
    }
}