﻿/* Copyright 2016-present MongoDB Inc.
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

using SAEA.Mongo.Bson.Serialization;

namespace SAEA.Mongo.Driver.GridFS
{
    /// <summary>
    /// Represents a serializer for a GridFSFileInfo.
    /// </summary>
    /// <typeparam name="TFileId">The type of the file identifier.</typeparam>
    public interface IGridFSFileInfoSerializer<TFileId> : IBsonSerializer<GridFSFileInfo<TFileId>>, IBsonDocumentSerializer
    {
    }
}