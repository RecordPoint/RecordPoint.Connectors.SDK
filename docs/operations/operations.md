## The Record Ingestion Process
A connector is made up of multiple roles that are responsible for performing the various activities related to record ingestion.

### Content Manager
See [Content Manager](./content_manager.md)

Your connector, with its many services and responsibilities needs any entry point to initiate the execution of the connectors various functions,
and the main point of orchestration for the Connectors SDK is the content manager.

The content manager is responsible for monitoring the known Connector Configurations and ensuring channel discovery operations are running for every known connector configuration.

### Channel Discovery
See [Channel Discovery](./channel_discovery.md)

The Connectors SDK is based upon the concept that all content sources have one or more high-level aggregations referred to as Channels.

A Channel is a grouping, aggregation, or collection of records within the content source. Channels will be used in later operations to poll the content source for changes (so we don't poll the entire content source at once).

> The channel concept allows your connector to manage record ingestion on small subsets of records from within the content source, opposed to dealing with larger volumes of records.<br>This is designed to limit the amount of processing work required for each execution of the running content synchronisation operations.

Each connector configuration will result in the continued execution of a channel discovery operation.

The channel discovery action should query the content source and discover channels.
When a channel has been discovered, the channel meta data is returned to the SDK which will spawn new content synchronisation operations for each newly discovered channel.
The action can also inform the SDK to spawn new content registration operations for various channels.

Channels returned via the Channel Discovery Action are stored within the Channel cache, and subsequent executions can return updated details for already discovered channels that will automatically be updated within the cache.

### Content Synchronisation
See [Content Synchronisation](./content_synchronisation.md)

Content Synchronisation is the act of polling the content source for changes since a previous known point and submitting the content to Records365.
The Connectors SDK implements this function via the content synchronisation operation.

Content synchronisation is an operation that periodically executes within the context of a given channel, checking the content source for new or updated records.
When new or updated records are found, the meta data is collected and returned to the SDk as a collection of records, binaries and aggregations.
The SDK then spawns appropriate operations to submit this content to Records365.

The `IContentSynchronisationAction` has two execution points `BeginAsync` and `ContinueAsync`.
The `BeginAsync` method is only called on the first execution of the operation for the given channel, while the `ContinueAsync` is called on every subsequent execution.
The first execution should establish a cursor that is used on subsquent executions to establish a point in which content deltas can be retrieved from the content source.

### Content Registration
See [Content Registration](./content_registration.md)

While content synchronisation is a continually running process designed to ingest all new record changes on the content source within a channel, content registration is the act submitting all historical content for a given channel.

Content registration behaves similar to content synchronisation, in that there are two execution methods for the `IContentRegistrationAction` interface.
The intention is that the action should:
* Query the content source at first execution, retrieving data from a given starting point.
* The content source should return a subset/paginated resultset of records, including a cursor.
* The action should return a small number of records/binaries/aggregations (as well as the cursor) to the SDK for submission.
* Subsequent executions should use the cursor to re-query the content source, ingesting more records at each execution.
* When all content up to a given point has been ingested, the content registration operation can be completed.

### Record Submission
See [Record Submission](./record_submission.md)

Execution of either a content synchronisation or content registration operation can result in new records that should be ingested to Records365.
The SDK handles record submission by spawning record submission operations for each record that has been requested to be ingested.

There is no code required to be implemented within the connector other than setting up the role within a deployable service via the relevent dependency injection extensions.

### Binary Submission
See [Binary Submission](./binary_submission.md)

Binary submission occurs as a result of content registration or content synchronisation operations finding new content to ingest.
Binaries are submitted to Records365 via a call to the Records365 Connector Api to obtain a connection string, and then using that connection string to upload the binary to blob storage.

The binary submission operation has an `IBinaryRetrievalAction` that must be implemented within your connector.
This action needs to obtain a stream to the binary and return it to the SDK so the binary can be uploaded.  

### Aggregation Submission
See [Aggregation Submission](./aggregation_submission.md)

Aggregations are low-level groupings of records within a content source channel.
As with record submission, no code is required to be implemented within the connector other than setting up the role within a deployable service via the relevent dependency injection extensions.

### Audit Submission
See [Audit Submission](./audit_submission.md)

### Record Disposal
See [Record Disposal](./record_disposal.md)

Record disposal is an operation that is initiated via notifications from Records365 and is intended to remove records from the Content Source when a disposal is actioned from within Records 365.
The record disposal operation will invoke the `IRecordDisposalAction` where the logic for deletion of the record within content source should be implemented.
