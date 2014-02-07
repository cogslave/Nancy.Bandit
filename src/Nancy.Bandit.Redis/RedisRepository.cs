
namespace Nancy.Bandit.Redis
{
    using BookSleeve;
    using Nancy.Bandit.Repository;
    using ProtoBuf;
    using System.IO;

    class RedisRepository : IRepository
    {
        private int databaseId;

        public Hypothesis SelectHypothesis(string key)
        {
            using (var connection = new RedisConnection("123.123.123.123"))
            {
                connection.Open();
                var data = connection.Strings.Get(databaseId, key);
                return Serializer.Deserialize<Hypothesis>(new MemoryStream(connection.Wait(data)));
            }
        }

        public void Synchronize()
        {
            throw new System.NotImplementedException();
        }
        
        //protected T PassThroughProtobuf<T>(T instance)
        //{
        //    // Serialize
        //    var memoryStream = new MemoryStream();
        //    ProtobufTypeModel.Serialize(memoryStream, instance);

        //    // Deserialize
        //    memoryStream.Seek(0, SeekOrigin.Begin);
        //    var result = (T)ProtobufTypeModel.Deserialize(memoryStream, null, typeof(T));

        //    // Small reference test
        //    Assert.AreNotSame(instance, result);

        //    // If you want to know what is being generated
        //    Console.WriteLine(HexDump.Dump(memoryStream.ToArray()));

        //    return result;
        //}

    }
}
