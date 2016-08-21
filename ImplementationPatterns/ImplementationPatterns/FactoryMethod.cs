using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public static class LogEntryParser
    {
        public static LogEntry Parse(string data)
        {
            //анализируем содержание data и создаем нужный
            //экземпляр: ExceptionLogEntry или SimpleLogEntry
            return new LogEntry();
        }

    }
        
    public abstract class LogReaderBase   
    {
        public IEnumerable<LogEntry> Read()
        {             
            using (var stream = OpenLogSource())
            {
                using (var reader = new StreamReader(stream))
                {
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return LogEntryParser.Parse(line);
                    }
                }
            }
            
        }

        protected abstract Stream OpenLogSource();
    }
   
    #region using delegate in the static factory

    static class ImporterFactory
    {
        private static readonly Dictionary<string, Func<Importer>> _map =
            new Dictionary<string, Func<Importer>>();

        static ImporterFactory()
        {
            _map[".json"] = () => new JsonImporter();
            _map[".xla"] = () => new XlsImporter();
            _map[".xlsx"] = () => new XlsImporter();
        }

        public static Importer Create(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            var creator = GetCreator(extension);
            if (creator == null)
            {
                throw new NotSupportedException();
            }

            return creator();
        }

        private static Func<Importer> GetCreator(string extension)
        {
            Func<Importer> creator;
            _map.TryGetValue(extension, out creator);

            return creator;
        }
    }

    public abstract class Importer
    {}

    public class JsonImporter : Importer { }
    public class XlsImporter : Importer { }

    #endregion


    #region Generic Factory

    public abstract class Product
    {
        protected internal abstract void PostConstruction();
    }

    public class ConcreteProduct : Product
    {
        //внутренний конструктор не позволит клиентам иерархии 
        //создавать объекты напрямую.
        internal ConcreteProduct() { }

        protected internal override void PostConstruction()
        {
            Console.WriteLine("ConcreteProduct: post constraction");
        }
    }

    //единственный способ создания объектов семейства Product
    public static class ProductFactory
    {
        public static T Create<T>() where T : Product, new()
        {
            try 
            {
                var t = new T();
                t.PostConstruction();

                return t;
            }
            catch (TargetInvocationException e)
            { 
                //разворачиваем исключение и бросаем исходное
                var edi = ExceptionDispatchInfo.Capture(e.InnerException);
                edi.Throw();

                //эта точка недостижима, но компилятор об этом знает
                return default(T);
            }
        }
    }

    // var p1 = ProductFactory.Create<ConctereProduct>();
    // var p2 = ProductFactory.Create<AnotherProduct>();

    #endregion

}
