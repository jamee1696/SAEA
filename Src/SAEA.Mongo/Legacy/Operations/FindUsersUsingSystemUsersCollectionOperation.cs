/* Copyright 2010-present MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SAEA.Mongo.Bson;
using SAEA.Mongo.Bson.Serialization.Serializers;
using SAEA.Mongo.Driver.Core.Bindings;
using SAEA.Mongo.Driver.Core.Operations;
using SAEA.Mongo.Driver.Core.WireProtocol.Messages.Encoders;

namespace SAEA.Mongo.Driver.Operations
{
    internal class FindUsersUsingSystemUsersCollectionOperation : IReadOperation<IEnumerable<BsonDocument>>
    {
        // fields
        private readonly DatabaseNamespace _databaseNamespace;
        private readonly MessageEncoderSettings _messageEncoderSettings;
        private readonly string _username;

        // constructors
        public FindUsersUsingSystemUsersCollectionOperation(
            DatabaseNamespace databaseNamespace,
            string username,
            MessageEncoderSettings messageEncoderSettings)
        {
            _databaseNamespace = databaseNamespace;
            _username = username;
            _messageEncoderSettings = messageEncoderSettings;
        }

        // methods
        public IEnumerable<BsonDocument> Execute(IReadBinding binding, CancellationToken cancellationToken)
        {
            var collectionNamespace = new CollectionNamespace(_databaseNamespace, "system.users");
            var filter = _username == null ? new BsonDocument() : new BsonDocument("user", _username);
            var operation = new FindOperation<BsonDocument>(collectionNamespace, BsonDocumentSerializer.Instance, _messageEncoderSettings)
            {
                Filter = filter,
                RetryRequested = false
            };
            var cursor = operation.Execute(binding, cancellationToken);
            return cursor.ToList(cancellationToken);
        }

        public Task<IEnumerable<BsonDocument>> ExecuteAsync(IReadBinding binding, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}