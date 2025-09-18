<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Client

## Contents

- [AggregationUnspecifiedFieldValuePipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-AggregationUnspecifiedFieldValuePipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.AggregationUnspecifiedFieldValuePipelineElement')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-AggregationUnspecifiedFieldValuePipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.AggregationUnspecifiedFieldValuePipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [GetRequiredStringFields()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-AggregationUnspecifiedFieldValuePipelineElement-GetRequiredStringFields 'RecordPoint.Connectors.SDK.SubmitPipeline.AggregationUnspecifiedFieldValuePipelineElement.GetRequiredStringFields')
- [ApiClient](#T-RecordPoint-Connectors-SDK-Client-ApiClient 'RecordPoint.Connectors.SDK.Client.ApiClient')
  - [#ctor(httpClient,disposeHttpClient)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Net-Http-HttpClient,System-Boolean- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(System.Net.Http.HttpClient,System.Boolean)')
  - [#ctor(handlers)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Net-Http-DelegatingHandler[]- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(System.Net.Http.DelegatingHandler[])')
  - [#ctor(rootHandler,handlers)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Net-Http-HttpClientHandler,System-Net-Http-DelegatingHandler[]- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(System.Net.Http.HttpClientHandler,System.Net.Http.DelegatingHandler[])')
  - [#ctor(baseUri,handlers)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Uri,System-Net-Http-DelegatingHandler[]- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(System.Uri,System.Net.Http.DelegatingHandler[])')
  - [#ctor(baseUri,rootHandler,handlers)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Uri,System-Net-Http-HttpClientHandler,System-Net-Http-DelegatingHandler[]- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(System.Uri,System.Net.Http.HttpClientHandler,System.Net.Http.DelegatingHandler[])')
  - [#ctor(credentials,handlers)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-Microsoft-Rest-ServiceClientCredentials,System-Net-Http-DelegatingHandler[]- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(Microsoft.Rest.ServiceClientCredentials,System.Net.Http.DelegatingHandler[])')
  - [#ctor(credentials,httpClient,disposeHttpClient)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-Microsoft-Rest-ServiceClientCredentials,System-Net-Http-HttpClient,System-Boolean- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(Microsoft.Rest.ServiceClientCredentials,System.Net.Http.HttpClient,System.Boolean)')
  - [#ctor(credentials,rootHandler,handlers)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-Microsoft-Rest-ServiceClientCredentials,System-Net-Http-HttpClientHandler,System-Net-Http-DelegatingHandler[]- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(Microsoft.Rest.ServiceClientCredentials,System.Net.Http.HttpClientHandler,System.Net.Http.DelegatingHandler[])')
  - [#ctor(baseUri,credentials,handlers)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Uri,Microsoft-Rest-ServiceClientCredentials,System-Net-Http-DelegatingHandler[]- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(System.Uri,Microsoft.Rest.ServiceClientCredentials,System.Net.Http.DelegatingHandler[])')
  - [#ctor(baseUri,credentials,rootHandler,handlers)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Uri,Microsoft-Rest-ServiceClientCredentials,System-Net-Http-HttpClientHandler,System-Net-Http-DelegatingHandler[]- 'RecordPoint.Connectors.SDK.Client.ApiClient.#ctor(System.Uri,Microsoft.Rest.ServiceClientCredentials,System.Net.Http.HttpClientHandler,System.Net.Http.DelegatingHandler[])')
  - [BaseUri](#P-RecordPoint-Connectors-SDK-Client-ApiClient-BaseUri 'RecordPoint.Connectors.SDK.Client.ApiClient.BaseUri')
  - [Credentials](#P-RecordPoint-Connectors-SDK-Client-ApiClient-Credentials 'RecordPoint.Connectors.SDK.Client.ApiClient.Credentials')
  - [DeserializationSettings](#P-RecordPoint-Connectors-SDK-Client-ApiClient-DeserializationSettings 'RecordPoint.Connectors.SDK.Client.ApiClient.DeserializationSettings')
  - [GET](#P-RecordPoint-Connectors-SDK-Client-ApiClient-GET 'RecordPoint.Connectors.SDK.Client.ApiClient.GET')
  - [POST](#P-RecordPoint-Connectors-SDK-Client-ApiClient-POST 'RecordPoint.Connectors.SDK.Client.ApiClient.POST')
  - [PUT](#P-RecordPoint-Connectors-SDK-Client-ApiClient-PUT 'RecordPoint.Connectors.SDK.Client.ApiClient.PUT')
  - [SerializationSettings](#P-RecordPoint-Connectors-SDK-Client-ApiClient-SerializationSettings 'RecordPoint.Connectors.SDK.Client.ApiClient.SerializationSettings')
  - [ApiBinariesPostWithHttpMessagesAndStreamAsync(connectorId,itemExternalId,binaryExternalId,fileName,correlationId,acceptLanguage,customHeaders,inputStream,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-ApiClient-ApiBinariesPostWithHttpMessagesAndStreamAsync-System-String,System-String,System-String,System-String,System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-IO-Stream,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.ApiClient.ApiBinariesPostWithHttpMessagesAndStreamAsync(System.String,System.String,System.String,System.String,System.String,System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.IO.Stream,System.Threading.CancellationToken)')
  - [Initialize()](#M-RecordPoint-Connectors-SDK-Client-ApiClient-Initialize 'RecordPoint.Connectors.SDK.Client.ApiClient.Initialize')
- [ApiClientFactory](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactory 'RecordPoint.Connectors.SDK.Client.ApiClientFactory')
  - [CreateApiClient(settings)](#M-RecordPoint-Connectors-SDK-Client-ApiClientFactory-CreateApiClient-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings- 'RecordPoint.Connectors.SDK.Client.ApiClientFactory.CreateApiClient(RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings)')
  - [CreateAuthenticationProvider()](#M-RecordPoint-Connectors-SDK-Client-ApiClientFactory-CreateAuthenticationProvider-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings- 'RecordPoint.Connectors.SDK.Client.ApiClientFactory.CreateAuthenticationProvider(RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings)')
  - [GetApiClient(settings)](#M-RecordPoint-Connectors-SDK-Client-ApiClientFactory-GetApiClient-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings- 'RecordPoint.Connectors.SDK.Client.ApiClientFactory.GetApiClient(RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings)')
- [ApiClientRetryPolicy](#T-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy 'RecordPoint.Connectors.SDK.Client.ApiClientRetryPolicy')
  - [_knownRetriableWebResponseStatusCodes](#F-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy-_knownRetriableWebResponseStatusCodes 'RecordPoint.Connectors.SDK.Client.ApiClientRetryPolicy._knownRetriableWebResponseStatusCodes')
  - [GetPolicy(maxTryCount,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy-GetPolicy-System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.ApiClientRetryPolicy.GetPolicy(System.Int32,System.Threading.CancellationToken)')
  - [GetPolicy(log,callerType,methodName,maxTryCount,logPrefix,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy-GetPolicy-RecordPoint-Connectors-SDK-Diagnostics-ILog,System-Type,System-String,System-Int32,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.ApiClientRetryPolicy.GetPolicy(RecordPoint.Connectors.SDK.Diagnostics.ILog,System.Type,System.String,System.Int32,System.String,System.Threading.CancellationToken)')
  - [IsRecords365ApiRetriableException(ex,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy-IsRecords365ApiRetriableException-System-Exception,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.ApiClientRetryPolicy.IsRecords365ApiRetriableException(System.Exception,System.Threading.CancellationToken)')
- [AuthenticationResult](#T-RecordPoint-Connectors-SDK-Client-AuthenticationResult 'RecordPoint.Connectors.SDK.Client.AuthenticationResult')
  - [AccessToken](#P-RecordPoint-Connectors-SDK-Client-AuthenticationResult-AccessToken 'RecordPoint.Connectors.SDK.Client.AuthenticationResult.AccessToken')
  - [AccessTokenType](#P-RecordPoint-Connectors-SDK-Client-AuthenticationResult-AccessTokenType 'RecordPoint.Connectors.SDK.Client.AuthenticationResult.AccessTokenType')
- [BinarySubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-ExternalId 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.ExternalId')
  - [FileHash](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-FileHash 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.FileHash')
  - [FileName](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-FileName 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.FileName')
  - [IsOldVersion](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-IsOldVersion 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.IsOldVersion')
  - [ItemExternalId](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-ItemExternalId 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.ItemExternalId')
  - [ItemSourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-ItemSourceLastModifiedDate 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.ItemSourceLastModifiedDate')
  - [MimeType](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-MimeType 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.MimeType')
  - [SkipEnrichment](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-SkipEnrichment 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.SkipEnrichment')
  - [SourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.SourceLastModifiedDate')
  - [Stream](#P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-Stream 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.Stream')
  - [GetExternalId()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-GetExternalId 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.GetExternalId')
  - [GetTitle()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-GetTitle 'RecordPoint.Connectors.SDK.SubmitPipeline.BinarySubmitContext.GetTitle')
- [CircuitBreakerOptions](#T-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions 'RecordPoint.Connectors.SDK.Providers.CircuitBreakerOptions')
  - [DurationOfBreakS](#P-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions-DurationOfBreakS 'RecordPoint.Connectors.SDK.Providers.CircuitBreakerOptions.DurationOfBreakS')
  - [FailureThreshold](#P-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions-FailureThreshold 'RecordPoint.Connectors.SDK.Providers.CircuitBreakerOptions.FailureThreshold')
  - [MinimumAttempts](#P-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions-MinimumAttempts 'RecordPoint.Connectors.SDK.Providers.CircuitBreakerOptions.MinimumAttempts')
  - [SamplingDurationS](#P-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions-SamplingDurationS 'RecordPoint.Connectors.SDK.Providers.CircuitBreakerOptions.SamplingDurationS')
- [ConfidentialClientAuthenticationProvider](#T-RecordPoint-Connectors-SDK-Client-ConfidentialClientAuthenticationProvider 'RecordPoint.Connectors.SDK.Client.ConfidentialClientAuthenticationProvider')
  - [#ctor(settings)](#M-RecordPoint-Connectors-SDK-Client-ConfidentialClientAuthenticationProvider-#ctor-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings- 'RecordPoint.Connectors.SDK.Client.ConfidentialClientAuthenticationProvider.#ctor(RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings)')
  - [AcquireTokenAsync(settings)](#M-RecordPoint-Connectors-SDK-Client-ConfidentialClientAuthenticationProvider-AcquireTokenAsync-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings- 'RecordPoint.Connectors.SDK.Client.ConfidentialClientAuthenticationProvider.AcquireTokenAsync(RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings)')
- [ConnectorConfigAccessor](#T-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigAccessor')
  - [ApiClientFactory](#P-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor-ApiClientFactory 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigAccessor.ApiClientFactory')
  - [ApiClientFactorySettings](#P-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigAccessor.ApiClientFactorySettings')
  - [AuthenticationHelperSettings](#P-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigAccessor.AuthenticationHelperSettings')
  - [GetConnectorConfig(connectorConfigId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor-GetConnectorConfig-System-Guid,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigAccessor.GetConnectorConfig(System.Guid,System.Threading.CancellationToken)')
- [ConnectorConfigEnabledStateHistory](#T-RecordPoint-Connectors-SDK-Client-ConnectorConfigEnabledStateHistory 'RecordPoint.Connectors.SDK.Client.ConnectorConfigEnabledStateHistory')
  - [HasBeenEnabled](#F-RecordPoint-Connectors-SDK-Client-ConnectorConfigEnabledStateHistory-HasBeenEnabled 'RecordPoint.Connectors.SDK.Client.ConnectorConfigEnabledStateHistory.HasBeenEnabled')
- [ConnectorConfigStatus](#T-RecordPoint-Connectors-SDK-Client-ConnectorConfigStatus 'RecordPoint.Connectors.SDK.Client.ConnectorConfigStatus')
  - [Disabled](#F-RecordPoint-Connectors-SDK-Client-ConnectorConfigStatus-Disabled 'RecordPoint.Connectors.SDK.Client.ConnectorConfigStatus.Disabled')
  - [Enabled](#F-RecordPoint-Connectors-SDK-Client-ConnectorConfigStatus-Enabled 'RecordPoint.Connectors.SDK.Client.ConnectorConfigStatus.Enabled')
  - [Error](#F-RecordPoint-Connectors-SDK-Client-ConnectorConfigStatus-Error 'RecordPoint.Connectors.SDK.Client.ConnectorConfigStatus.Error')
- [ConnectorNotificationModelExtensions](#T-RecordPoint-Connectors-SDK-Client-ConnectorNotificationModelExtensions 'RecordPoint.Connectors.SDK.Client.ConnectorNotificationModelExtensions')
  - [ToAcknowledge(model,processingResult,message)](#M-RecordPoint-Connectors-SDK-Client-ConnectorNotificationModelExtensions-ToAcknowledge-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,RecordPoint-Connectors-SDK-Client-ProcessingResult,System-String- 'RecordPoint.Connectors.SDK.Client.ConnectorNotificationModelExtensions.ToAcknowledge(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,RecordPoint.Connectors.SDK.Client.ProcessingResult,System.String)')
- [DateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-DateTimeProvider 'RecordPoint.Connectors.SDK.Providers.DateTimeProvider')
  - [Instance](#P-RecordPoint-Connectors-SDK-Providers-DateTimeProvider-Instance 'RecordPoint.Connectors.SDK.Providers.DateTimeProvider.Instance')
  - [UtcNow](#P-RecordPoint-Connectors-SDK-Providers-DateTimeProvider-UtcNow 'RecordPoint.Connectors.SDK.Providers.DateTimeProvider.UtcNow')
- [DirectSubmitBinaryPipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,System-Boolean- 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission,System.Boolean)')
  - [BlobFactory](#P-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-BlobFactory 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement.BlobFactory')
  - [CircuitProvider](#P-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-CircuitProvider 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement.CircuitProvider')
  - [RetryProvider](#P-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-RetryProvider 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement.RetryProvider')
  - [DefaultBlobFactory(url)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-DefaultBlobFactory-System-String- 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement.DefaultBlobFactory(System.String)')
  - [EscapeBlobMetaDataValue(value)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-EscapeBlobMetaDataValue-System-String- 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement.EscapeBlobMetaDataValue(System.String)')
  - [InnerHandleSuccessfulSubmissionAsync(submitContext,result,binarySubmissionInputModel,apiClient,retryPolicy)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-InnerHandleSuccessfulSubmissionAsync-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,Microsoft-Rest-HttpOperationResponse{System-Object},RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,RecordPoint-Connectors-SDK-Client-IApiClient,Polly-Retry-AsyncRetryPolicy- 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement.InnerHandleSuccessfulSubmissionAsync(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext,Microsoft.Rest.HttpOperationResponse{System.Object},RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel,RecordPoint.Connectors.SDK.Client.IApiClient,Polly.Retry.AsyncRetryPolicy)')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.DirectSubmitBinaryPipelineElement.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [ExceptionHelper](#T-RecordPoint-Connectors-SDK-Helpers-ExceptionHelper 'RecordPoint.Connectors.SDK.Helpers.ExceptionHelper')
  - [IsAssignableFrom\`\`1(ex,code)](#M-RecordPoint-Connectors-SDK-Helpers-ExceptionHelper-IsAssignableFrom``1-System-Exception,System-Func{``0,System-Boolean}- 'RecordPoint.Connectors.SDK.Helpers.ExceptionHelper.IsAssignableFrom``1(System.Exception,System.Func{``0,System.Boolean})')
  - [IsTaskCancellation(ex,cancellationToken)](#M-RecordPoint-Connectors-SDK-Helpers-ExceptionHelper-IsTaskCancellation-System-Exception,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Helpers.ExceptionHelper.IsTaskCancellation(System.Exception,System.Threading.CancellationToken)')
- [Extensions](#T-RecordPoint-Connectors-SDK-Client-Extensions 'RecordPoint.Connectors.SDK.Client.Extensions')
  - [TryAddQueryParameter(queryParameters,value,format)](#M-RecordPoint-Connectors-SDK-Client-Extensions-TryAddQueryParameter-System-Collections-Generic-List{System-String},System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Extensions.TryAddQueryParameter(System.Collections.Generic.List{System.String},System.String,System.String)')
- [FilterPipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-FilterPipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.FilterPipelineElement')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-FilterPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.FilterPipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-FilterPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.FilterPipelineElement.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [GET](#T-RecordPoint-Connectors-SDK-Client-GET 'RecordPoint.Connectors.SDK.Client.GET')
  - [#ctor(client)](#M-RecordPoint-Connectors-SDK-Client-GET-#ctor-RecordPoint-Connectors-SDK-Client-ApiClient- 'RecordPoint.Connectors.SDK.Client.GET.#ctor(RecordPoint.Connectors.SDK.Client.ApiClient)')
  - [Client](#P-RecordPoint-Connectors-SDK-Client-GET-Client 'RecordPoint.Connectors.SDK.Client.GET.Client')
  - [ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,connectorId,pageSize,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(System.String,System.String,System.Nullable{System.Guid},System.Nullable{System.Int32},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,pagesize,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync(System.String,System.String,System.Nullable{System.Int32},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync-System-Nullable{System-Guid},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync(System.Nullable{System.Guid},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiConnectorConfigurationsWithHttpMessagesAsync(acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiConnectorConfigurationsWithHttpMessagesAsync-System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiConnectorConfigurationsWithHttpMessagesAsync(System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiConnectorConfigurationsidWithHttpMessagesAsync(id,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiConnectorConfigurationsidWithHttpMessagesAsync-System-Guid,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiConnectorConfigurationsidWithHttpMessagesAsync(System.Guid,System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,connectorId,pageSize,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(System.String,System.String,System.Nullable{System.Guid},System.Nullable{System.Int32},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiItemsfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,pagesize,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiItemsfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiItemsfieldNamefieldValueWithHttpMessagesAsync(System.String,System.String,System.Nullable{System.Int32},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiNotificationsWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiNotificationsWithHttpMessagesAsync-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiNotificationsWithHttpMessagesAsync(System.String,System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiNotificationsconnectorIdWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GET-ApiNotificationsconnectorIdWithHttpMessagesAsync-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GET.ApiNotificationsconnectorIdWithHttpMessagesAsync(System.String,System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
- [GETExtensions](#T-RecordPoint-Connectors-SDK-Client-GETExtensions 'RecordPoint.Connectors.SDK.Client.GETExtensions')
  - [ApiAggregationsMultiTenantedfieldNamefieldValue(operations,fieldName,fieldValue,connectorId,pageSize,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiAggregationsMultiTenantedfieldNamefieldValue-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiAggregationsMultiTenantedfieldNamefieldValue(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Nullable{System.Guid},System.Nullable{System.Int32},System.String)')
  - [ApiAggregationsMultiTenantedfieldNamefieldValueAsync(operations,fieldName,fieldValue,connectorId,pageSize,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiAggregationsMultiTenantedfieldNamefieldValueAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiAggregationsMultiTenantedfieldNamefieldValueAsync(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Nullable{System.Guid},System.Nullable{System.Int32},System.String,System.Threading.CancellationToken)')
  - [ApiAggregationsfieldNamefieldValue(operations,fieldName,fieldValue,pagesize,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiAggregationsfieldNamefieldValue-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Int32},System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiAggregationsfieldNamefieldValue(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Nullable{System.Int32},System.String)')
  - [ApiAggregationsfieldNamefieldValueAsync(operations,fieldName,fieldValue,pagesize,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiAggregationsfieldNamefieldValueAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Int32},System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiAggregationsfieldNamefieldValueAsync(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Nullable{System.Int32},System.String,System.Threading.CancellationToken)')
  - [ApiConnectorConfigurations(operations,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurations-RecordPoint-Connectors-SDK-Client-IGET,System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiConnectorConfigurations(RecordPoint.Connectors.SDK.Client.IGET,System.String)')
  - [ApiConnectorConfigurationsAsync(operations,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiConnectorConfigurationsAsync(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.Threading.CancellationToken)')
  - [ApiConnectorConfigurationsGetMultiTenanted(operations,connectorId,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsGetMultiTenanted-RecordPoint-Connectors-SDK-Client-IGET,System-Nullable{System-Guid},System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiConnectorConfigurationsGetMultiTenanted(RecordPoint.Connectors.SDK.Client.IGET,System.Nullable{System.Guid},System.String)')
  - [ApiConnectorConfigurationsGetMultiTenantedAsync(operations,connectorId,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsGetMultiTenantedAsync-RecordPoint-Connectors-SDK-Client-IGET,System-Nullable{System-Guid},System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiConnectorConfigurationsGetMultiTenantedAsync(RecordPoint.Connectors.SDK.Client.IGET,System.Nullable{System.Guid},System.String,System.Threading.CancellationToken)')
  - [ApiConnectorConfigurationsid(operations,id,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsid-RecordPoint-Connectors-SDK-Client-IGET,System-Guid,System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiConnectorConfigurationsid(RecordPoint.Connectors.SDK.Client.IGET,System.Guid,System.String)')
  - [ApiConnectorConfigurationsidAsync(operations,id,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsidAsync-RecordPoint-Connectors-SDK-Client-IGET,System-Guid,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiConnectorConfigurationsidAsync(RecordPoint.Connectors.SDK.Client.IGET,System.Guid,System.String,System.Threading.CancellationToken)')
  - [ApiItemsMultiTenantedfieldNamefieldValue(operations,fieldName,fieldValue,connectorId,pageSize,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiItemsMultiTenantedfieldNamefieldValue-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiItemsMultiTenantedfieldNamefieldValue(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Nullable{System.Guid},System.Nullable{System.Int32},System.String)')
  - [ApiItemsMultiTenantedfieldNamefieldValueAsync(operations,fieldName,fieldValue,connectorId,pageSize,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiItemsMultiTenantedfieldNamefieldValueAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiItemsMultiTenantedfieldNamefieldValueAsync(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Nullable{System.Guid},System.Nullable{System.Int32},System.String,System.Threading.CancellationToken)')
  - [ApiItemsfieldNamefieldValue(operations,fieldName,fieldValue,pagesize,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiItemsfieldNamefieldValue-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Int32},System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiItemsfieldNamefieldValue(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Nullable{System.Int32},System.String)')
  - [ApiItemsfieldNamefieldValueAsync(operations,fieldName,fieldValue,pagesize,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiItemsfieldNamefieldValueAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Int32},System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiItemsfieldNamefieldValueAsync(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Nullable{System.Int32},System.String,System.Threading.CancellationToken)')
  - [ApiNotifications(operations,connectorId,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiNotifications-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiNotifications(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String)')
  - [ApiNotificationsAsync(operations,connectorId,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiNotificationsAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiNotificationsAsync(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Threading.CancellationToken)')
  - [ApiNotificationsconnectorId(operations,connectorId,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiNotificationsconnectorId-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiNotificationsconnectorId(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String)')
  - [ApiNotificationsconnectorIdAsync(operations,connectorId,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiNotificationsconnectorIdAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.GETExtensions.ApiNotificationsconnectorIdAsync(RecordPoint.Connectors.SDK.Client.IGET,System.String,System.String,System.Threading.CancellationToken)')
- [HTTPRequestHelper](#T-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper 'RecordPoint.Connectors.SDK.Helpers.HTTPRequestHelper')
  - [_knownRetriableWebExceptionStatuses](#F-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper-_knownRetriableWebExceptionStatuses 'RecordPoint.Connectors.SDK.Helpers.HTTPRequestHelper._knownRetriableWebExceptionStatuses')
  - [GetHttpRequestHeaders(authProvider,settings)](#M-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper-GetHttpRequestHeaders-RecordPoint-Connectors-SDK-Client-IAuthenticationProvider,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings- 'RecordPoint.Connectors.SDK.Helpers.HTTPRequestHelper.GetHttpRequestHeaders(RecordPoint.Connectors.SDK.Client.IAuthenticationProvider,RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings)')
  - [IsRetriableWebRequestException(ex)](#M-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper-IsRetriableWebRequestException-System-Exception- 'RecordPoint.Connectors.SDK.Helpers.HTTPRequestHelper.IsRetriableWebRequestException(System.Exception)')
  - [IsWebExceptionStatusRetriable(wex)](#M-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper-IsWebExceptionStatusRetriable-System-Net-WebException- 'RecordPoint.Connectors.SDK.Helpers.HTTPRequestHelper.IsWebExceptionStatusRetriable(System.Net.WebException)')
- [HttpHeaderExtension](#T-RecordPoint-Connectors-SDK-Client-HttpHeaderExtension 'RecordPoint.Connectors.SDK.Client.HttpHeaderExtension')
  - [AuthorizationHeaderName](#F-RecordPoint-Connectors-SDK-Client-HttpHeaderExtension-AuthorizationHeaderName 'RecordPoint.Connectors.SDK.Client.HttpHeaderExtension.AuthorizationHeaderName')
  - [AddAuthorizationHeader(headers,tokenType,token)](#M-RecordPoint-Connectors-SDK-Client-HttpHeaderExtension-AddAuthorizationHeader-System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-String,System-String- 'RecordPoint.Connectors.SDK.Client.HttpHeaderExtension.AddAuthorizationHeader(System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.String,System.String)')
- [HttpSubmitAggregationPipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAggregationPipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitAggregationPipelineElement')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAggregationPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitAggregationPipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAggregationPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitAggregationPipelineElement.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [HttpSubmitAuditEventPipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAuditEventPipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitAuditEventPipelineElement')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAuditEventPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitAuditEventPipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAuditEventPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitAuditEventPipelineElement.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [HttpSubmitBinaryPipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitBinaryPipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitBinaryPipelineElement')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitBinaryPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitBinaryPipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitBinaryPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitBinaryPipelineElement.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [HttpSubmitItemPipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitItemPipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitItemPipelineElement')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitItemPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitItemPipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitItemPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitItemPipelineElement.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [HttpSubmitPipelineElementBase](#T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitPipelineElementBase')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitPipelineElementBase.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [ApiClientFactory](#P-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase-ApiClientFactory 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitPipelineElementBase.ApiClientFactory')
  - [GetRetryPolicy()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase-GetRetryPolicy-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitPipelineElementBase.GetRetryPolicy(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext,System.String)')
  - [HandleSubmitResponse\`\`1(submitContext,result,itemTypeName)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase-HandleSubmitResponse``1-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,Microsoft-Rest-HttpOperationResponse{``0},System-String- 'RecordPoint.Connectors.SDK.SubmitPipeline.HttpSubmitPipelineElementBase.HandleSubmitResponse``1(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext,Microsoft.Rest.HttpOperationResponse{``0},System.String)')
- [IApiClient](#T-RecordPoint-Connectors-SDK-Client-IApiClient 'RecordPoint.Connectors.SDK.Client.IApiClient')
  - [BaseUri](#P-RecordPoint-Connectors-SDK-Client-IApiClient-BaseUri 'RecordPoint.Connectors.SDK.Client.IApiClient.BaseUri')
  - [Credentials](#P-RecordPoint-Connectors-SDK-Client-IApiClient-Credentials 'RecordPoint.Connectors.SDK.Client.IApiClient.Credentials')
  - [DeserializationSettings](#P-RecordPoint-Connectors-SDK-Client-IApiClient-DeserializationSettings 'RecordPoint.Connectors.SDK.Client.IApiClient.DeserializationSettings')
  - [GET](#P-RecordPoint-Connectors-SDK-Client-IApiClient-GET 'RecordPoint.Connectors.SDK.Client.IApiClient.GET')
  - [POST](#P-RecordPoint-Connectors-SDK-Client-IApiClient-POST 'RecordPoint.Connectors.SDK.Client.IApiClient.POST')
  - [PUT](#P-RecordPoint-Connectors-SDK-Client-IApiClient-PUT 'RecordPoint.Connectors.SDK.Client.IApiClient.PUT')
  - [SerializationSettings](#P-RecordPoint-Connectors-SDK-Client-IApiClient-SerializationSettings 'RecordPoint.Connectors.SDK.Client.IApiClient.SerializationSettings')
  - [ApiBinariesPostWithHttpMessagesAndStreamAsync(connectorId,itemExternalId,binaryExternalId,fileName,correlationId,acceptLanguage,customHeaders,inputStream,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IApiClient-ApiBinariesPostWithHttpMessagesAndStreamAsync-System-String,System-String,System-String,System-String,System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-IO-Stream,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IApiClient.ApiBinariesPostWithHttpMessagesAndStreamAsync(System.String,System.String,System.String,System.String,System.String,System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.IO.Stream,System.Threading.CancellationToken)')
- [IApiClientFactory](#T-RecordPoint-Connectors-SDK-Client-IApiClientFactory 'RecordPoint.Connectors.SDK.Client.IApiClientFactory')
  - [CreateApiClient(settings)](#M-RecordPoint-Connectors-SDK-Client-IApiClientFactory-CreateApiClient-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings- 'RecordPoint.Connectors.SDK.Client.IApiClientFactory.CreateApiClient(RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings)')
  - [CreateAuthenticationProvider(settings)](#M-RecordPoint-Connectors-SDK-Client-IApiClientFactory-CreateAuthenticationProvider-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings- 'RecordPoint.Connectors.SDK.Client.IApiClientFactory.CreateAuthenticationProvider(RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings)')
- [IAuthenticationProvider](#T-RecordPoint-Connectors-SDK-Client-IAuthenticationProvider 'RecordPoint.Connectors.SDK.Client.IAuthenticationProvider')
  - [AcquireTokenAsync(settings)](#M-RecordPoint-Connectors-SDK-Client-IAuthenticationProvider-AcquireTokenAsync-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings- 'RecordPoint.Connectors.SDK.Client.IAuthenticationProvider.AcquireTokenAsync(RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings)')
- [IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET')
  - [ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,connectorId,pageSize,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(System.String,System.String,System.Nullable{System.Guid},System.Nullable{System.Int32},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,pagesize,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync(System.String,System.String,System.Nullable{System.Int32},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync-System-Nullable{System-Guid},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync(System.Nullable{System.Guid},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiConnectorConfigurationsWithHttpMessagesAsync(acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiConnectorConfigurationsWithHttpMessagesAsync-System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiConnectorConfigurationsWithHttpMessagesAsync(System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiConnectorConfigurationsidWithHttpMessagesAsync(id,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiConnectorConfigurationsidWithHttpMessagesAsync-System-Guid,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiConnectorConfigurationsidWithHttpMessagesAsync(System.Guid,System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,connectorId,pageSize,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(System.String,System.String,System.Nullable{System.Guid},System.Nullable{System.Int32},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiItemsfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,pagesize,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiItemsfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiItemsfieldNamefieldValueWithHttpMessagesAsync(System.String,System.String,System.Nullable{System.Int32},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiNotificationsWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiNotificationsWithHttpMessagesAsync-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiNotificationsWithHttpMessagesAsync(System.String,System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiNotificationsconnectorIdWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IGET-ApiNotificationsconnectorIdWithHttpMessagesAsync-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IGET.ApiNotificationsconnectorIdWithHttpMessagesAsync(System.String,System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
- [IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST')
  - [ApiAggregationsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IPOST-ApiAggregationsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IPOST.ApiAggregationsWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiBinariesGetSASTokenWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IPOST-ApiBinariesGetSASTokenWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IPOST.ApiBinariesGetSASTokenWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IPOST-ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IPOST.ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiBinariesWithHttpMessagesAsync(connectorId,itemExternalId,binaryExternalId,fileName,location,correlationId,isOldVersion,skipEnrichment,itemSourceLastModifiedDate,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IPOST-ApiBinariesWithHttpMessagesAsync-System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IPOST.ApiBinariesWithHttpMessagesAsync(System.String,System.String,System.String,System.String,System.String,System.String,System.Nullable{System.Boolean},System.Nullable{System.Boolean},System.Nullable{System.DateTime},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiItemsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IPOST-ApiItemsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IPOST.ApiItemsWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiNotificationsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IPOST-ApiNotificationsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IPOST.ApiNotificationsWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
- [IPUT](#T-RecordPoint-Connectors-SDK-Client-IPUT 'RecordPoint.Connectors.SDK.Client.IPUT')
  - [ApiAuditEventsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IPUT-ApiAuditEventsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IPUT.ApiAuditEventsWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
- [ItemSubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ItemSubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.ItemSubmitContext')
  - [BinariesSubmitted](#P-RecordPoint-Connectors-SDK-SubmitPipeline-ItemSubmitContext-BinariesSubmitted 'RecordPoint.Connectors.SDK.SubmitPipeline.ItemSubmitContext.BinariesSubmitted')
- [ItemUnspecifiedFieldValuePipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ItemUnspecifiedFieldValuePipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.ItemUnspecifiedFieldValuePipelineElement')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-ItemUnspecifiedFieldValuePipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.ItemUnspecifiedFieldValuePipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [GetRequiredStringFields()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-ItemUnspecifiedFieldValuePipelineElement-GetRequiredStringFields 'RecordPoint.Connectors.SDK.SubmitPipeline.ItemUnspecifiedFieldValuePipelineElement.GetRequiredStringFields')
- [LockHelper](#T-RecordPoint-Connectors-SDK-Helpers-LockHelper 'RecordPoint.Connectors.SDK.Helpers.LockHelper')
  - [LockedCreateOrUpdate\`\`1(lockingObject,lockedObject,lockCondition,lockedObjectFactory)](#M-RecordPoint-Connectors-SDK-Helpers-LockHelper-LockedCreateOrUpdate``1-System-Object@,``0@,System-Func{``0,System-Boolean},System-Func{``0,``0}- 'RecordPoint.Connectors.SDK.Helpers.LockHelper.LockedCreateOrUpdate``1(System.Object@,``0@,System.Func{``0,System.Boolean},System.Func{``0,``0})')
  - [LockedGetOrCreate\`\`1(lockingObject,lockedObject,lockedObjectFactory)](#M-RecordPoint-Connectors-SDK-Helpers-LockHelper-LockedGetOrCreate``1-System-Object@,``0@,System-Func{``0}- 'RecordPoint.Connectors.SDK.Helpers.LockHelper.LockedGetOrCreate``1(System.Object@,``0@,System.Func{``0})')
  - [LockedRead\`\`1(lockingObject,lockedObject)](#M-RecordPoint-Connectors-SDK-Helpers-LockHelper-LockedRead``1-System-Object@,``0@- 'RecordPoint.Connectors.SDK.Helpers.LockHelper.LockedRead``1(System.Object@,``0@)')
- [MatchResult](#T-RecordPoint-Connectors-SDK-Filters-MatchResult 'RecordPoint.Connectors.SDK.Filters.MatchResult')
  - [MatchReason](#P-RecordPoint-Connectors-SDK-Filters-MatchResult-MatchReason 'RecordPoint.Connectors.SDK.Filters.MatchResult.MatchReason')
  - [Result](#P-RecordPoint-Connectors-SDK-Filters-MatchResult-Result 'RecordPoint.Connectors.SDK.Filters.MatchResult.Result')
- [NotSpecifiedCredentials](#T-RecordPoint-Connectors-SDK-Client-NotSpecifiedCredentials 'RecordPoint.Connectors.SDK.Client.NotSpecifiedCredentials')
  - [InnerProcessHttpRequestAsync(request,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-NotSpecifiedCredentials-InnerProcessHttpRequestAsync-System-Net-Http-HttpRequestMessage,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.NotSpecifiedCredentials.InnerProcessHttpRequestAsync(System.Net.Http.HttpRequestMessage,System.Threading.CancellationToken)')
  - [ProcessHttpRequestAsync(request,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-NotSpecifiedCredentials-ProcessHttpRequestAsync-System-Net-Http-HttpRequestMessage,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.NotSpecifiedCredentials.ProcessHttpRequestAsync(System.Net.Http.HttpRequestMessage,System.Threading.CancellationToken)')
- [NotificationPullManager](#T-RecordPoint-Connectors-SDK-Notifications-NotificationPullManager 'RecordPoint.Connectors.SDK.Notifications.NotificationPullManager')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Notifications-NotificationPullManager-#ctor 'RecordPoint.Connectors.SDK.Notifications.NotificationPullManager.#ctor')
  - [AcknowledgeNotification(factorySettings,authenticationSettings,acknowledgement,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationPullManager-AcknowledgeNotification-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.NotificationPullManager.AcknowledgeNotification(RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings,RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings,RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel,System.Threading.CancellationToken)')
  - [GetAllPendingConnectorNotifications(factorySettings,authenticationSettings,connectorConfigId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationPullManager-GetAllPendingConnectorNotifications-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.NotificationPullManager.GetAllPendingConnectorNotifications(RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings,RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings,System.String,System.Threading.CancellationToken)')
- [NotificationType](#T-RecordPoint-Connectors-SDK-Client-NotificationType 'RecordPoint.Connectors.SDK.Client.NotificationType')
  - [ConnectorConfigCreated](#F-RecordPoint-Connectors-SDK-Client-NotificationType-ConnectorConfigCreated 'RecordPoint.Connectors.SDK.Client.NotificationType.ConnectorConfigCreated')
  - [ConnectorConfigDeleted](#F-RecordPoint-Connectors-SDK-Client-NotificationType-ConnectorConfigDeleted 'RecordPoint.Connectors.SDK.Client.NotificationType.ConnectorConfigDeleted')
  - [ConnectorConfigUpdated](#F-RecordPoint-Connectors-SDK-Client-NotificationType-ConnectorConfigUpdated 'RecordPoint.Connectors.SDK.Client.NotificationType.ConnectorConfigUpdated')
  - [ItemDestroyed](#F-RecordPoint-Connectors-SDK-Client-NotificationType-ItemDestroyed 'RecordPoint.Connectors.SDK.Client.NotificationType.ItemDestroyed')
  - [Ping](#F-RecordPoint-Connectors-SDK-Client-NotificationType-Ping 'RecordPoint.Connectors.SDK.Client.NotificationType.Ping')
- [POST](#T-RecordPoint-Connectors-SDK-Client-POST 'RecordPoint.Connectors.SDK.Client.POST')
  - [#ctor(client)](#M-RecordPoint-Connectors-SDK-Client-POST-#ctor-RecordPoint-Connectors-SDK-Client-ApiClient- 'RecordPoint.Connectors.SDK.Client.POST.#ctor(RecordPoint.Connectors.SDK.Client.ApiClient)')
  - [Client](#P-RecordPoint-Connectors-SDK-Client-POST-Client 'RecordPoint.Connectors.SDK.Client.POST.Client')
  - [ApiAggregationsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POST-ApiAggregationsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POST.ApiAggregationsWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiBinariesGetSASTokenWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POST-ApiBinariesGetSASTokenWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POST.ApiBinariesGetSASTokenWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POST-ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POST.ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiBinariesWithHttpMessagesAsync(connectorId,itemExternalId,binaryExternalId,fileName,location,correlationId,isOldVersion,skipEnrichment,itemSourceLastModifiedDate,acceptLanguage,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POST-ApiBinariesWithHttpMessagesAsync-System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POST.ApiBinariesWithHttpMessagesAsync(System.String,System.String,System.String,System.String,System.String,System.String,System.Nullable{System.Boolean},System.Nullable{System.Boolean},System.Nullable{System.DateTime},System.String,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiItemsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POST-ApiItemsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POST.ApiItemsWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
  - [ApiNotificationsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POST-ApiNotificationsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POST.ApiNotificationsWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
- [POSTExtensions](#T-RecordPoint-Connectors-SDK-Client-POSTExtensions 'RecordPoint.Connectors.SDK.Client.POSTExtensions')
  - [ApiAggregations(operations,acceptLanguage,body)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiAggregations-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiAggregations(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel)')
  - [ApiAggregationsAsync(operations,acceptLanguage,body,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiAggregationsAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiAggregationsAsync(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel,System.Threading.CancellationToken)')
  - [ApiBinaries(operations,connectorId,itemExternalId,binaryExternalId,fileName,location,correlationId,isOldVersion,skipEnrichment,itemSourceLastModifiedDate,acceptLanguage)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinaries-RecordPoint-Connectors-SDK-Client-IPOST,System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime},System-String- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiBinaries(RecordPoint.Connectors.SDK.Client.IPOST,System.String,System.String,System.String,System.String,System.String,System.String,System.Nullable{System.Boolean},System.Nullable{System.Boolean},System.Nullable{System.DateTime},System.String)')
  - [ApiBinariesAsync(operations,connectorId,itemExternalId,binaryExternalId,fileName,location,correlationId,isOldVersion,skipEnrichment,itemSourceLastModifiedDate,acceptLanguage,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime},System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiBinariesAsync(RecordPoint.Connectors.SDK.Client.IPOST,System.String,System.String,System.String,System.String,System.String,System.String,System.Nullable{System.Boolean},System.Nullable{System.Boolean},System.Nullable{System.DateTime},System.String,System.Threading.CancellationToken)')
  - [ApiBinariesGetSASToken(operations,acceptLanguage,body)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesGetSASToken-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiBinariesGetSASToken(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel)')
  - [ApiBinariesGetSASTokenAsync(operations,acceptLanguage,body,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesGetSASTokenAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiBinariesGetSASTokenAsync(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel,System.Threading.CancellationToken)')
  - [ApiBinariesNotifyBinarySubmission(operations,acceptLanguage,body)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesNotifyBinarySubmission-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiBinariesNotifyBinarySubmission(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel)')
  - [ApiBinariesNotifyBinarySubmissionAsync(operations,acceptLanguage,body,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesNotifyBinarySubmissionAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiBinariesNotifyBinarySubmissionAsync(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel,System.Threading.CancellationToken)')
  - [ApiItems(operations,acceptLanguage,body)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiItems-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiItems(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel)')
  - [ApiItemsAsync(operations,acceptLanguage,body,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiItemsAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiItemsAsync(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel,System.Threading.CancellationToken)')
  - [ApiNotifications(operations,acceptLanguage,body)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiNotifications-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiNotifications(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel)')
  - [ApiNotificationsAsync(operations,acceptLanguage,body,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiNotificationsAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.POSTExtensions.ApiNotificationsAsync(RecordPoint.Connectors.SDK.Client.IPOST,System.String,RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel,System.Threading.CancellationToken)')
- [PUT](#T-RecordPoint-Connectors-SDK-Client-PUT 'RecordPoint.Connectors.SDK.Client.PUT')
  - [#ctor(client)](#M-RecordPoint-Connectors-SDK-Client-PUT-#ctor-RecordPoint-Connectors-SDK-Client-ApiClient- 'RecordPoint.Connectors.SDK.Client.PUT.#ctor(RecordPoint.Connectors.SDK.Client.ApiClient)')
  - [Client](#P-RecordPoint-Connectors-SDK-Client-PUT-Client 'RecordPoint.Connectors.SDK.Client.PUT.Client')
  - [ApiAuditEventsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-PUT-ApiAuditEventsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.PUT.ApiAuditEventsWithHttpMessagesAsync(System.String,RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel,System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}},System.Threading.CancellationToken)')
- [PUTExtensions](#T-RecordPoint-Connectors-SDK-Client-PUTExtensions 'RecordPoint.Connectors.SDK.Client.PUTExtensions')
  - [ApiAuditEvents(operations,acceptLanguage,body)](#M-RecordPoint-Connectors-SDK-Client-PUTExtensions-ApiAuditEvents-RecordPoint-Connectors-SDK-Client-IPUT,System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel- 'RecordPoint.Connectors.SDK.Client.PUTExtensions.ApiAuditEvents(RecordPoint.Connectors.SDK.Client.IPUT,System.String,RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel)')
  - [ApiAuditEventsAsync(operations,acceptLanguage,body,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-PUTExtensions-ApiAuditEventsAsync-RecordPoint-Connectors-SDK-Client-IPUT,System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.PUTExtensions.ApiAuditEventsAsync(RecordPoint.Connectors.SDK.Client.IPUT,System.String,RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel,System.Threading.CancellationToken)')
- [PipelineSelectorPipelineElement](#T-RecordPoint-Connectors-SDK-SubmitPipeline-PipelineSelectorPipelineElement 'RecordPoint.Connectors.SDK.SubmitPipeline.PipelineSelectorPipelineElement')
  - [#ctor(submitRecord,submitAggregation)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-PipelineSelectorPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.PipelineSelectorPipelineElement.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission,RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-PipelineSelectorPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.PipelineSelectorPipelineElement.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [SubmitPipelineElementBase](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [Log](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-Log 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase.Log')
  - [InvokeNext(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-InvokeNext-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase.InvokeNext(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
  - [LogMessage(context,methodName,message)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-LogMessage-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String,System-String- 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase.LogMessage(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext,System.String,System.String)')
  - [LogVerbose(context,methodName,message)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-LogVerbose-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String,System-String- 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase.LogVerbose(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext,System.String,System.String)')
  - [LogWarning(context,methodName,message)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-LogWarning-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String,System-String- 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase.LogWarning(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext,System.String,System.String)')
  - [SkipNext(submitContext,reason)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-SkipNext-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String- 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase.SkipNext(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext,System.String)')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitPipelineElementBase.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [UnspecifiedFieldValuePipelineElementBase](#T-RecordPoint-Connectors-SDK-SubmitPipeline-UnspecifiedFieldValuePipelineElementBase 'RecordPoint.Connectors.SDK.SubmitPipeline.UnspecifiedFieldValuePipelineElementBase')
  - [#ctor(next)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-UnspecifiedFieldValuePipelineElementBase-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.SubmitPipeline.UnspecifiedFieldValuePipelineElementBase.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [GetRequiredStringFields()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-UnspecifiedFieldValuePipelineElementBase-GetRequiredStringFields 'RecordPoint.Connectors.SDK.SubmitPipeline.UnspecifiedFieldValuePipelineElementBase.GetRequiredStringFields')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-UnspecifiedFieldValuePipelineElementBase-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.UnspecifiedFieldValuePipelineElementBase.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-AggregationUnspecifiedFieldValuePipelineElement'></a>
## AggregationUnspecifiedFieldValuePipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A submit pipeline element that ensures that required fields for aggregations have a value of "Unspecified".

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-AggregationUnspecifiedFieldValuePipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new AggregationUnspecifiedFieldValuePipelineElement with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-AggregationUnspecifiedFieldValuePipelineElement-GetRequiredStringFields'></a>
### GetRequiredStringFields() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Client-ApiClient'></a>
## ApiClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Why is this here?

The OpenAPI/Swagger 2.0 tooling for providing binary/file uploads to HTTP methods is not great -
it only supports multipart/form-data which is not suitable for our needs. 

The autogenerated client in ApiClient therefore doesn't provide any way to provide a Stream
to the binary submission endpoint. Hence, we provide a copy+paste of that method here,
with an additional parameter for the Stream.

This should be revisited as/when we upgrade to OpenAPI/Swagger 3.0.

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Net-Http-HttpClient,System-Boolean-'></a>
### #ctor(httpClient,disposeHttpClient) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| httpClient | [System.Net.Http.HttpClient](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpClient 'System.Net.Http.HttpClient') | HttpClient to be used |
| disposeHttpClient | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | True: will dispose the provided httpClient on calling ApiClient.Dispose(). False: will not dispose provided httpClient |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Net-Http-DelegatingHandler[]-'></a>
### #ctor(handlers) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| handlers | [System.Net.Http.DelegatingHandler[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.DelegatingHandler[] 'System.Net.Http.DelegatingHandler[]') | Optional. The delegating handlers to add to the http client pipeline. |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Net-Http-HttpClientHandler,System-Net-Http-DelegatingHandler[]-'></a>
### #ctor(rootHandler,handlers) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rootHandler | [System.Net.Http.HttpClientHandler](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpClientHandler 'System.Net.Http.HttpClientHandler') | Optional. The http client handler used to handle http transport. |
| handlers | [System.Net.Http.DelegatingHandler[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.DelegatingHandler[] 'System.Net.Http.DelegatingHandler[]') | Optional. The delegating handlers to add to the http client pipeline. |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Uri,System-Net-Http-DelegatingHandler[]-'></a>
### #ctor(baseUri,handlers) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseUri | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. The base URI of the service. |
| handlers | [System.Net.Http.DelegatingHandler[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.DelegatingHandler[] 'System.Net.Http.DelegatingHandler[]') | Optional. The delegating handlers to add to the http client pipeline. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Uri,System-Net-Http-HttpClientHandler,System-Net-Http-DelegatingHandler[]-'></a>
### #ctor(baseUri,rootHandler,handlers) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseUri | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. The base URI of the service. |
| rootHandler | [System.Net.Http.HttpClientHandler](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpClientHandler 'System.Net.Http.HttpClientHandler') | Optional. The http client handler used to handle http transport. |
| handlers | [System.Net.Http.DelegatingHandler[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.DelegatingHandler[] 'System.Net.Http.DelegatingHandler[]') | Optional. The delegating handlers to add to the http client pipeline. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-Microsoft-Rest-ServiceClientCredentials,System-Net-Http-DelegatingHandler[]-'></a>
### #ctor(credentials,handlers) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| credentials | [Microsoft.Rest.ServiceClientCredentials](#T-Microsoft-Rest-ServiceClientCredentials 'Microsoft.Rest.ServiceClientCredentials') | Required. Subscription credentials which uniquely identify client subscription. |
| handlers | [System.Net.Http.DelegatingHandler[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.DelegatingHandler[] 'System.Net.Http.DelegatingHandler[]') | Optional. The delegating handlers to add to the http client pipeline. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-Microsoft-Rest-ServiceClientCredentials,System-Net-Http-HttpClient,System-Boolean-'></a>
### #ctor(credentials,httpClient,disposeHttpClient) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| credentials | [Microsoft.Rest.ServiceClientCredentials](#T-Microsoft-Rest-ServiceClientCredentials 'Microsoft.Rest.ServiceClientCredentials') | Required. Subscription credentials which uniquely identify client subscription. |
| httpClient | [System.Net.Http.HttpClient](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpClient 'System.Net.Http.HttpClient') | HttpClient to be used |
| disposeHttpClient | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | True: will dispose the provided httpClient on calling ApiClient.Dispose(). False: will not dispose provided httpClient |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-Microsoft-Rest-ServiceClientCredentials,System-Net-Http-HttpClientHandler,System-Net-Http-DelegatingHandler[]-'></a>
### #ctor(credentials,rootHandler,handlers) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| credentials | [Microsoft.Rest.ServiceClientCredentials](#T-Microsoft-Rest-ServiceClientCredentials 'Microsoft.Rest.ServiceClientCredentials') | Required. Subscription credentials which uniquely identify client subscription. |
| rootHandler | [System.Net.Http.HttpClientHandler](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpClientHandler 'System.Net.Http.HttpClientHandler') | Optional. The http client handler used to handle http transport. |
| handlers | [System.Net.Http.DelegatingHandler[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.DelegatingHandler[] 'System.Net.Http.DelegatingHandler[]') | Optional. The delegating handlers to add to the http client pipeline. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Uri,Microsoft-Rest-ServiceClientCredentials,System-Net-Http-DelegatingHandler[]-'></a>
### #ctor(baseUri,credentials,handlers) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseUri | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. The base URI of the service. |
| credentials | [Microsoft.Rest.ServiceClientCredentials](#T-Microsoft-Rest-ServiceClientCredentials 'Microsoft.Rest.ServiceClientCredentials') | Required. Subscription credentials which uniquely identify client subscription. |
| handlers | [System.Net.Http.DelegatingHandler[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.DelegatingHandler[] 'System.Net.Http.DelegatingHandler[]') | Optional. The delegating handlers to add to the http client pipeline. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-#ctor-System-Uri,Microsoft-Rest-ServiceClientCredentials,System-Net-Http-HttpClientHandler,System-Net-Http-DelegatingHandler[]-'></a>
### #ctor(baseUri,credentials,rootHandler,handlers) `constructor`

##### Summary

Initializes a new instance of the ApiClient class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseUri | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. The base URI of the service. |
| credentials | [Microsoft.Rest.ServiceClientCredentials](#T-Microsoft-Rest-ServiceClientCredentials 'Microsoft.Rest.ServiceClientCredentials') | Required. Subscription credentials which uniquely identify client subscription. |
| rootHandler | [System.Net.Http.HttpClientHandler](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpClientHandler 'System.Net.Http.HttpClientHandler') | Optional. The http client handler used to handle http transport. |
| handlers | [System.Net.Http.DelegatingHandler[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.DelegatingHandler[] 'System.Net.Http.DelegatingHandler[]') | Optional. The delegating handlers to add to the http client pipeline. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClient-BaseUri'></a>
### BaseUri `property`

##### Summary

The base URI of the service.

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClient-Credentials'></a>
### Credentials `property`

##### Summary

Subscription credentials which uniquely identify client subscription.

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClient-DeserializationSettings'></a>
### DeserializationSettings `property`

##### Summary

Gets or sets json deserialization settings.

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClient-GET'></a>
### GET `property`

##### Summary

Gets the IGET.

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClient-POST'></a>
### POST `property`

##### Summary

Gets the IPOST.

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClient-PUT'></a>
### PUT `property`

##### Summary

Gets the IPUT.

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClient-SerializationSettings'></a>
### SerializationSettings `property`

##### Summary

Gets or sets json serialization settings.

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-ApiBinariesPostWithHttpMessagesAndStreamAsync-System-String,System-String,System-String,System-String,System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-IO-Stream,System-Threading-CancellationToken-'></a>
### ApiBinariesPostWithHttpMessagesAndStreamAsync(connectorId,itemExternalId,binaryExternalId,fileName,correlationId,acceptLanguage,customHeaders,inputStream,cancellationToken) `method`

##### Summary

Submits a binary to be archived and protected by Records365 vNext.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ID of the connector submitting the binary |
| itemExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ExternalID of the item that the binary belongs to |
| binaryExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ExternalID of the binary |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An optional file name to associate with the binary |
| correlationId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An optional ID used to track the binary as it moves through the pipeline |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| inputStream | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') | A stream for binary data to upload. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClient-Initialize'></a>
### Initialize() `method`

##### Summary

Initializes client properties.

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Client-ApiClientFactory'></a>
## ApiClientFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Creates a singleton ApiClient

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClientFactory-CreateApiClient-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings-'></a>
### CreateApiClient(settings) `method`

##### Summary

Creates an IApiClient.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| settings | [RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClientFactory-CreateAuthenticationProvider-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-'></a>
### CreateAuthenticationProvider() `method`

##### Summary

Returns a singleton AuthenticationHelper

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClientFactory-GetApiClient-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings-'></a>
### GetApiClient(settings) `method`

##### Summary

Returns a singleton ApiClient instance

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| settings | [RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy'></a>
## ApiClientRetryPolicy `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Helpers for handling retries against the Records365 vNext Connector API.

<a name='F-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy-_knownRetriableWebResponseStatusCodes'></a>
### _knownRetriableWebResponseStatusCodes `constants`

##### Summary

A list of HTTP response codes that are retriable.

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy-GetPolicy-System-Int32,System-Threading-CancellationToken-'></a>
### GetPolicy(maxTryCount,cancellationToken) `method`

##### Summary

Gets a retry policy for use when calling the Records365 vNext Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maxTryCount | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy-GetPolicy-RecordPoint-Connectors-SDK-Diagnostics-ILog,System-Type,System-String,System-Int32,System-String,System-Threading-CancellationToken-'></a>
### GetPolicy(log,callerType,methodName,maxTryCount,logPrefix,cancellationToken) `method`

##### Summary

Gets a retry policy for use when calling the Records365 vNext Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| log | [RecordPoint.Connectors.SDK.Diagnostics.ILog](#T-RecordPoint-Connectors-SDK-Diagnostics-ILog 'RecordPoint.Connectors.SDK.Diagnostics.ILog') |  |
| callerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| maxTryCount | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| logPrefix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-ApiClientRetryPolicy-IsRecords365ApiRetriableException-System-Exception,System-Threading-CancellationToken-'></a>
### IsRecords365ApiRetriableException(ex,cancellationToken) `method`

##### Summary

Determines if the exception is considered retriable in the context of accessing the Records365 vNext
Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-AuthenticationResult'></a>
## AuthenticationResult `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

The result of an authentication attempt.

<a name='P-RecordPoint-Connectors-SDK-Client-AuthenticationResult-AccessToken'></a>
### AccessToken `property`

##### Summary

The access token.

<a name='P-RecordPoint-Connectors-SDK-Client-AuthenticationResult-AccessTokenType'></a>
### AccessTokenType `property`

##### Summary

The type of the access token, e.g., "Bearer".

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext'></a>
## BinarySubmitContext `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A SubmitContext to be used specifically when submitting binaries 
to the Records365 vNext Connector API. Use with the HttpSubmitBinaryPipelineElement.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-ExternalId'></a>
### ExternalId `property`

##### Summary

The ExternalId of the binary.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-FileHash'></a>
### FileHash `property`

##### Summary

An optional MD5 hash of the binary content.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-FileName'></a>
### FileName `property`

##### Summary

An optional FileName for the binary.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-IsOldVersion'></a>
### IsOldVersion `property`

##### Summary

(Optional) Indicates whether the binary is the latest or an older version

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-ItemExternalId'></a>
### ItemExternalId `property`

##### Summary

The ExternalId of the Item that the submitted binary is to be associated with.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-ItemSourceLastModifiedDate'></a>
### ItemSourceLastModifiedDate `property`

##### Summary

(Optional) The source last modified date of the corresponding item submission.
This must match the item submission as it is used to match the binary submission to the item.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-MimeType'></a>
### MimeType `property`

##### Summary

An optional MimeType for the binary.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-SkipEnrichment'></a>
### SkipEnrichment `property`

##### Summary

(Optional) Indicates whether the binary will skip the enrichment pipeline

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `property`

##### Summary

The Source Last Modified date for the record, used to assocate the item version to the binary.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-Stream'></a>
### Stream `property`

##### Summary

A readable stream of binary data to be included in the submit.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-GetExternalId'></a>
### GetExternalId() `method`

##### Summary

Retrieve the External ID from the strongly typed ExternalId field instead of the Core Metadata for binaries

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-BinarySubmitContext-GetTitle'></a>
### GetTitle() `method`

##### Summary

Retrieve the title from the strongly typed FileName field instead of the Core Metadata for binaries

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions'></a>
## CircuitBreakerOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary

The circuit breaker options.

<a name='P-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions-DurationOfBreakS'></a>
### DurationOfBreakS `property`

##### Summary

1 - 922337203685 (Timespan.MaxValue = ~ 922337203685.4s)
How long the circuit will remain open after breaking.

<a name='P-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions-FailureThreshold'></a>
### FailureThreshold `property`

##### Summary

0.01 - 1
What proportion of calls which throw Circuit Breaker exceptions have to 
fail before the circuit is tripped. 0 would trip the circuit breaker 
on the first failure, 1 would only trip the circuit breaker if 100% of the calls
failed, 0.5 would trip the circuit breaker if 50% of the calls failed.

<a name='P-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions-MinimumAttempts'></a>
### MinimumAttempts `property`

##### Summary

2 - 2147483647 (Int 32.MaxValue)
The number of attempts within a period which have to be made for a circuit to consider
the number of failures to be count towards opening the circuit. For example, 
if the MinimumAttempts is 10 and the failure threshold is 0.5, it takes 10 consecutive failed 
attempts before the circuit breaker will trip, despite the failure threshold being 50%.
PLEASE NOTE: Circuit breakers are per-service, services are spread across multiple
nodes. Keep this in mind when determining a value for minimum attempts, as even with tens of
thousands of submissions a day, we're still only likely to get single digit attempts on a 
single node per 30s increment.

<a name='P-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions-SamplingDurationS'></a>
### SamplingDurationS `property`

##### Summary

2 - 922337203685 (Timespan.MaxValue = ~ 922337203685.4s)
How long a period to sample for failures in. The amount of calls which failed with
a Circuit Breaker exception is averaged across this period of time, and if the proportion 
of failed calls is greater than FailureThreshold, then the circuit breaks open.

<a name='T-RecordPoint-Connectors-SDK-Client-ConfidentialClientAuthenticationProvider'></a>
## ConfidentialClientAuthenticationProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Helper for acquiring access tokens from Azure AD.

<a name='M-RecordPoint-Connectors-SDK-Client-ConfidentialClientAuthenticationProvider-#ctor-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-'></a>
### #ctor(settings) `constructor`

##### Summary

Creates and initilizes a ConfidentialClientApplication

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| settings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-ConfidentialClientAuthenticationProvider-AcquireTokenAsync-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-'></a>
### AcquireTokenAsync(settings) `method`

##### Summary

Acquires an OAuth 2.0 access token from Azure AD for use with the Records365 Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| settings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |

<a name='T-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor'></a>
## ConnectorConfigAccessor `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

Gets a connector configuration by ID from the Records365 Connector API.

<a name='P-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor-ApiClientFactory'></a>
### ApiClientFactory `property`

##### Summary

The ApiClientFactory to use to call the Records365 Connector API.

<a name='P-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor-ApiClientFactorySettings'></a>
### ApiClientFactorySettings `property`

##### Summary

Settings used to call into the Records365 Connector API.

<a name='P-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor-AuthenticationHelperSettings'></a>
### AuthenticationHelperSettings `property`

##### Summary

The configuration that ConnectorConfigAccessor uses authenticate to the Records365 Connector API.

<a name='M-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigAccessor-GetConnectorConfig-System-Guid,System-Threading-CancellationToken-'></a>
### GetConnectorConfig(connectorConfigId,cancellationToken) `method`

##### Summary

Gets the ConnectorConfigModel from the Records365 Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfigId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-ConnectorConfigEnabledStateHistory'></a>
## ConnectorConfigEnabledStateHistory `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

The connector config enabled state history.

<a name='F-RecordPoint-Connectors-SDK-Client-ConnectorConfigEnabledStateHistory-HasBeenEnabled'></a>
### HasBeenEnabled `constants`

##### Summary

Has been enabled.

<a name='T-RecordPoint-Connectors-SDK-Client-ConnectorConfigStatus'></a>
## ConnectorConfigStatus `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Defines constant values that may appear in the ConnectorConfigModel.Status field.

<a name='F-RecordPoint-Connectors-SDK-Client-ConnectorConfigStatus-Disabled'></a>
### Disabled `constants`

##### Summary

Indicates the connector is in the Disabled status.
The connector is expected to do nothing in this status.

<a name='F-RecordPoint-Connectors-SDK-Client-ConnectorConfigStatus-Enabled'></a>
### Enabled `constants`

##### Summary

Indicates the Connector is in the Enabled status.
The connector is expected to operate as normal in this status.

<a name='F-RecordPoint-Connectors-SDK-Client-ConnectorConfigStatus-Error'></a>
### Error `constants`

##### Summary

Indicates the connector is in the Error status.
The connector is expected to do nothing in this status.

<a name='T-RecordPoint-Connectors-SDK-Client-ConnectorNotificationModelExtensions'></a>
## ConnectorNotificationModelExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

The connector notification model extensions.

<a name='M-RecordPoint-Connectors-SDK-Client-ConnectorNotificationModelExtensions-ToAcknowledge-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,RecordPoint-Connectors-SDK-Client-ProcessingResult,System-String-'></a>
### ToAcknowledge(model,processingResult,message) `method`

##### Summary

Creates a ConnectorNotificationAcknowledgeModel to be used to acknowledge the results
of processing of a connector notification.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| model | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| processingResult | [RecordPoint.Connectors.SDK.Client.ProcessingResult](#T-RecordPoint-Connectors-SDK-Client-ProcessingResult 'RecordPoint.Connectors.SDK.Client.ProcessingResult') |  |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-RecordPoint-Connectors-SDK-Providers-DateTimeProvider'></a>
## DateTimeProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary

Singleton implementation of IDateTimeProvider.

<a name='P-RecordPoint-Connectors-SDK-Providers-DateTimeProvider-Instance'></a>
### Instance `property`

##### Summary

Singleton instance.

<a name='P-RecordPoint-Connectors-SDK-Providers-DateTimeProvider-UtcNow'></a>
### UtcNow `property`

##### Summary

Gets a System.DateTime object that is set to the current date and time on this
computer, expressed as the Coordinated Universal Time (UTC).

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement'></a>
## DirectSubmitBinaryPipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

Pipeline element stream binary directly to blob storage

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,System-Boolean-'></a>
### #ctor() `constructor`

##### Summary

Constructor

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-BlobFactory'></a>
### BlobFactory `property`

##### Summary

Function which returns a CloudBlob any binaries will be saved into.
Override the default for testing purposes.
Note that when resolving classes from LightInject, defaults are not respected and LightInject will be unable to 
resolve this class without the DefaultBlobFactory function being registered as well.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-CircuitProvider'></a>
### CircuitProvider `property`

##### Summary

Circuit provider for handling backpressure for Azure Blob Storage

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-RetryProvider'></a>
### RetryProvider `property`

##### Summary

Retry Provider for handling backpressure for Azure Blob Storage

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-DefaultBlobFactory-System-String-'></a>
### DefaultBlobFactory(url) `method`

##### Summary

Default blob factory used for Production workloads

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-EscapeBlobMetaDataValue-System-String-'></a>
### EscapeBlobMetaDataValue(value) `method`

##### Summary

https://docs.microsoft.com/en-us/rest/api/storageservices/Setting-and-Retrieving-Properties-and-Metadata-for-Blob-Resources#Subheading1

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-InnerHandleSuccessfulSubmissionAsync-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,Microsoft-Rest-HttpOperationResponse{System-Object},RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,RecordPoint-Connectors-SDK-Client-IApiClient,Polly-Retry-AsyncRetryPolicy-'></a>
### InnerHandleSuccessfulSubmissionAsync(submitContext,result,binarySubmissionInputModel,apiClient,retryPolicy) `method`

##### Summary

Handles the result of a successful submission and determines if the pipeline should continue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |
| result | [Microsoft.Rest.HttpOperationResponse{System.Object}](#T-Microsoft-Rest-HttpOperationResponse{System-Object} 'Microsoft.Rest.HttpOperationResponse{System.Object}') |  |
| binarySubmissionInputModel | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') |  |
| apiClient | [RecordPoint.Connectors.SDK.Client.IApiClient](#T-RecordPoint-Connectors-SDK-Client-IApiClient 'RecordPoint.Connectors.SDK.Client.IApiClient') |  |
| retryPolicy | [Polly.Retry.AsyncRetryPolicy](#T-Polly-Retry-AsyncRetryPolicy 'Polly.Retry.AsyncRetryPolicy') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-DirectSubmitBinaryPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-Helpers-ExceptionHelper'></a>
## ExceptionHelper `type`

##### Namespace

RecordPoint.Connectors.SDK.Helpers

##### Summary

Provides helpers for dealing with exceptions.

<a name='M-RecordPoint-Connectors-SDK-Helpers-ExceptionHelper-IsAssignableFrom``1-System-Exception,System-Func{``0,System-Boolean}-'></a>
### IsAssignableFrom\`\`1(ex,code) `method`

##### Summary

Determines if an exception is of a given type, or if it is an AggregateException
that contains an exception of the given type.
If the item is of the given type, it must also satisfy the condition specified
in code

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |
| code | [System.Func{\`\`0,System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,System.Boolean}') |  |

<a name='M-RecordPoint-Connectors-SDK-Helpers-ExceptionHelper-IsTaskCancellation-System-Exception,System-Threading-CancellationToken-'></a>
### IsTaskCancellation(ex,cancellationToken) `method`

##### Summary

The C# HttpClient throws TaskCanceledExceptions for timeouts.
This ensures that any TaskCanceledException is a genuine exception that
has originated from a cancelled task (associated with the input CancellationToken)
rather than a timeout.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-Extensions'></a>
## Extensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

The extensions.

<a name='M-RecordPoint-Connectors-SDK-Client-Extensions-TryAddQueryParameter-System-Collections-Generic-List{System-String},System-String,System-String-'></a>
### TryAddQueryParameter(queryParameters,value,format) `method`

##### Summary

Try add query parameter.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queryParameters | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') | The query parameters. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value. |
| format | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The format. |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-FilterPipelineElement'></a>
## FilterPipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary



<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-FilterPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-FilterPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-GET'></a>
## GET `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

GET operations.

<a name='M-RecordPoint-Connectors-SDK-Client-GET-#ctor-RecordPoint-Connectors-SDK-Client-ApiClient-'></a>
### #ctor(client) `constructor`

##### Summary

Initializes a new instance of the GET class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [RecordPoint.Connectors.SDK.Client.ApiClient](#T-RecordPoint-Connectors-SDK-Client-ApiClient 'RecordPoint.Connectors.SDK.Client.ApiClient') | Reference to the service client. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='P-RecordPoint-Connectors-SDK-Client-GET-Client'></a>
### Client `property`

##### Summary

Gets a reference to the ApiClient

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,connectorId,pageSize,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of aggregations that match a single metadata field value
for multi Tenanted Connectors

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| pageSize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,pagesize,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of aggregations that match a single metadata field value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| pagesize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync-System-Nullable{System-Guid},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a connector configure model via connector ID MultiTenanted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiConnectorConfigurationsWithHttpMessagesAsync-System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsWithHttpMessagesAsync(acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Get all connectors for the given clientId across all tenants

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiConnectorConfigurationsidWithHttpMessagesAsync-System-Guid,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsidWithHttpMessagesAsync(id,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a connector configure model by its connector ID.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,connectorId,pageSize,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of items that match a single metadata field value for
MutiTenanted Environment

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| pageSize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiItemsfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiItemsfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,pagesize,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of items that match a single metadata field value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| pagesize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiNotificationsWithHttpMessagesAsync-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiNotificationsWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of notifications that are awaiting processing and
acknowledgement by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Connector ID for multi tenanted connections |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-GET-ApiNotificationsconnectorIdWithHttpMessagesAsync-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiNotificationsconnectorIdWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of notifications that are awaiting processing and
acknowledgement by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The query information |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='T-RecordPoint-Connectors-SDK-Client-GETExtensions'></a>
## GETExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Extension methods for GET.

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiAggregationsMultiTenantedfieldNamefieldValue-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String-'></a>
### ApiAggregationsMultiTenantedfieldNamefieldValue(operations,fieldName,fieldValue,connectorId,pageSize,acceptLanguage) `method`

##### Summary

Gets a collection of aggregations that match a single metadata field value
for multi Tenanted Connectors

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| pageSize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiAggregationsMultiTenantedfieldNamefieldValueAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Threading-CancellationToken-'></a>
### ApiAggregationsMultiTenantedfieldNamefieldValueAsync(operations,fieldName,fieldValue,connectorId,pageSize,acceptLanguage,cancellationToken) `method`

##### Summary

Gets a collection of aggregations that match a single metadata field value
for multi Tenanted Connectors

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| pageSize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiAggregationsfieldNamefieldValue-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Int32},System-String-'></a>
### ApiAggregationsfieldNamefieldValue(operations,fieldName,fieldValue,pagesize,acceptLanguage) `method`

##### Summary

Gets a collection of aggregations that match a single metadata field value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| pagesize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiAggregationsfieldNamefieldValueAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Int32},System-String,System-Threading-CancellationToken-'></a>
### ApiAggregationsfieldNamefieldValueAsync(operations,fieldName,fieldValue,pagesize,acceptLanguage,cancellationToken) `method`

##### Summary

Gets a collection of aggregations that match a single metadata field value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| pagesize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurations-RecordPoint-Connectors-SDK-Client-IGET,System-String-'></a>
### ApiConnectorConfigurations(operations,acceptLanguage) `method`

##### Summary

Get all connectors for the given clientId across all tenants

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsAsync(operations,acceptLanguage,cancellationToken) `method`

##### Summary

Get all connectors for the given clientId across all tenants

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsGetMultiTenanted-RecordPoint-Connectors-SDK-Client-IGET,System-Nullable{System-Guid},System-String-'></a>
### ApiConnectorConfigurationsGetMultiTenanted(operations,connectorId,acceptLanguage) `method`

##### Summary

Gets a connector configure model via connector ID MultiTenanted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsGetMultiTenantedAsync-RecordPoint-Connectors-SDK-Client-IGET,System-Nullable{System-Guid},System-String,System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsGetMultiTenantedAsync(operations,connectorId,acceptLanguage,cancellationToken) `method`

##### Summary

Gets a connector configure model via connector ID MultiTenanted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsid-RecordPoint-Connectors-SDK-Client-IGET,System-Guid,System-String-'></a>
### ApiConnectorConfigurationsid(operations,id,acceptLanguage) `method`

##### Summary

Gets a connector configure model by its connector ID.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| id | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiConnectorConfigurationsidAsync-RecordPoint-Connectors-SDK-Client-IGET,System-Guid,System-String,System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsidAsync(operations,id,acceptLanguage,cancellationToken) `method`

##### Summary

Gets a connector configure model by its connector ID.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| id | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiItemsMultiTenantedfieldNamefieldValue-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String-'></a>
### ApiItemsMultiTenantedfieldNamefieldValue(operations,fieldName,fieldValue,connectorId,pageSize,acceptLanguage) `method`

##### Summary

Gets a collection of items that match a single metadata field value for
MutiTenanted Environment

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| pageSize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiItemsMultiTenantedfieldNamefieldValueAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Threading-CancellationToken-'></a>
### ApiItemsMultiTenantedfieldNamefieldValueAsync(operations,fieldName,fieldValue,connectorId,pageSize,acceptLanguage,cancellationToken) `method`

##### Summary

Gets a collection of items that match a single metadata field value for
MutiTenanted Environment

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| pageSize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiItemsfieldNamefieldValue-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Int32},System-String-'></a>
### ApiItemsfieldNamefieldValue(operations,fieldName,fieldValue,pagesize,acceptLanguage) `method`

##### Summary

Gets a collection of items that match a single metadata field value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| pagesize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiItemsfieldNamefieldValueAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Nullable{System-Int32},System-String,System-Threading-CancellationToken-'></a>
### ApiItemsfieldNamefieldValueAsync(operations,fieldName,fieldValue,pagesize,acceptLanguage,cancellationToken) `method`

##### Summary

Gets a collection of items that match a single metadata field value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| pagesize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiNotifications-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String-'></a>
### ApiNotifications(operations,connectorId,acceptLanguage) `method`

##### Summary

Gets a collection of notifications that are awaiting processing and
acknowledgement by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Connector ID for multi tenanted connections |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiNotificationsAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Threading-CancellationToken-'></a>
### ApiNotificationsAsync(operations,connectorId,acceptLanguage,cancellationToken) `method`

##### Summary

Gets a collection of notifications that are awaiting processing and
acknowledgement by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Connector ID for multi tenanted connections |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiNotificationsconnectorId-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String-'></a>
### ApiNotificationsconnectorId(operations,connectorId,acceptLanguage) `method`

##### Summary

Gets a collection of notifications that are awaiting processing and
acknowledgement by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The query information |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-GETExtensions-ApiNotificationsconnectorIdAsync-RecordPoint-Connectors-SDK-Client-IGET,System-String,System-String,System-Threading-CancellationToken-'></a>
### ApiNotificationsconnectorIdAsync(operations,connectorId,acceptLanguage,cancellationToken) `method`

##### Summary

Gets a collection of notifications that are awaiting processing and
acknowledgement by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IGET](#T-RecordPoint-Connectors-SDK-Client-IGET 'RecordPoint.Connectors.SDK.Client.IGET') | The operations group for this extension method. |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The query information |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper'></a>
## HTTPRequestHelper `type`

##### Namespace

RecordPoint.Connectors.SDK.Helpers

##### Summary

The HTTP request helper.

<a name='F-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper-_knownRetriableWebExceptionStatuses'></a>
### _knownRetriableWebExceptionStatuses `constants`

##### Summary

The known retriable web exception statuses.

<a name='M-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper-GetHttpRequestHeaders-RecordPoint-Connectors-SDK-Client-IAuthenticationProvider,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-'></a>
### GetHttpRequestHeaders(authProvider,settings) `method`

##### Summary

Get Headers with Bearer Token

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| authProvider | [RecordPoint.Connectors.SDK.Client.IAuthenticationProvider](#T-RecordPoint-Connectors-SDK-Client-IAuthenticationProvider 'RecordPoint.Connectors.SDK.Client.IAuthenticationProvider') |  |
| settings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |

<a name='M-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper-IsRetriableWebRequestException-System-Exception-'></a>
### IsRetriableWebRequestException(ex) `method`

##### Summary

Checks if is retriable web request exception.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The ex. |

<a name='M-RecordPoint-Connectors-SDK-Helpers-HTTPRequestHelper-IsWebExceptionStatusRetriable-System-Net-WebException-'></a>
### IsWebExceptionStatusRetriable(wex) `method`

##### Summary

Checks if is web exception status retriable.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| wex | [System.Net.WebException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.WebException 'System.Net.WebException') | The wex. |

<a name='T-RecordPoint-Connectors-SDK-Client-HttpHeaderExtension'></a>
## HttpHeaderExtension `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

The http header extension.

<a name='F-RecordPoint-Connectors-SDK-Client-HttpHeaderExtension-AuthorizationHeaderName'></a>
### AuthorizationHeaderName `constants`

##### Summary

The authorization header name.

<a name='M-RecordPoint-Connectors-SDK-Client-HttpHeaderExtension-AddAuthorizationHeader-System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-String,System-String-'></a>
### AddAuthorizationHeader(headers,tokenType,token) `method`

##### Summary

Add authorization header.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| headers | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers. |
| tokenType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The token type. |
| token | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') |  |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAggregationPipelineElement'></a>
## HttpSubmitAggregationPipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A submit pipeline element that submits aggregations via Records365 vNext Connector API.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAggregationPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new HttpSubmitAggregationPipelineElement with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAggregationPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary

Submits an aggregation to the Records365 vNext Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAuditEventPipelineElement'></a>
## HttpSubmitAuditEventPipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A submit pipeline element that submits audit events via Records365 vNext Connector API.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAuditEventPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new HttpSubmitAuditEventPipelineElement with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitAuditEventPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary

Submits an audit event via the Records365 vNext Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitBinaryPipelineElement'></a>
## HttpSubmitBinaryPipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A submit pipeline element that submits item binaries via Records365 vNext Connector API.
DEPRECATED - please use DirectSubmitBinaryPipelineElement instead(if possible to do so).

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitBinaryPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new HttpSubmitBinaryPipelineElement with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitBinaryPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary

Submits an item binary via the Records365 vNext Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitItemPipelineElement'></a>
## HttpSubmitItemPipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A submit pipeline element that submits items via Records365 vNext Connector API.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitItemPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new HttpSubmitItemPipelineElement with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitItemPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary

Submits an item via the Records365 vNext Connector API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase'></a>
## HttpSubmitPipelineElementBase `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

Base class for submit pipeline elements that submit content to the Records365 vNext
Connector API.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new HttpSubmitPipelineElementBase with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase-ApiClientFactory'></a>
### ApiClientFactory `property`

##### Summary

Constructs the APIClientFactory used to communicate with Records365

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase-GetRetryPolicy-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String-'></a>
### GetRetryPolicy() `method`

##### Summary

Gets a retry policy which can be used for communicating with Records365

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-HttpSubmitPipelineElementBase-HandleSubmitResponse``1-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,Microsoft-Rest-HttpOperationResponse{``0},System-String-'></a>
### HandleSubmitResponse\`\`1(submitContext,result,itemTypeName) `method`

##### Summary

Handles the result of a call to a submission API.

##### Returns

True if the submit pipeline should continue as a result of successful submission, false otherwise.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') | The context of the submission. |
| result | [Microsoft.Rest.HttpOperationResponse{\`\`0}](#T-Microsoft-Rest-HttpOperationResponse{``0} 'Microsoft.Rest.HttpOperationResponse{``0}') | The result of the submit API call. |
| itemTypeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A string that identifies the item type (e.g., "Aggregation"). Used only for constructing log strings. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='T-RecordPoint-Connectors-SDK-Client-IApiClient'></a>
## IApiClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

This is the Records365 vNext Connector API Home

<a name='P-RecordPoint-Connectors-SDK-Client-IApiClient-BaseUri'></a>
### BaseUri `property`

##### Summary

The base URI of the service.

<a name='P-RecordPoint-Connectors-SDK-Client-IApiClient-Credentials'></a>
### Credentials `property`

##### Summary

Subscription credentials which uniquely identify client
subscription.

<a name='P-RecordPoint-Connectors-SDK-Client-IApiClient-DeserializationSettings'></a>
### DeserializationSettings `property`

##### Summary

Gets or sets json deserialization settings.

<a name='P-RecordPoint-Connectors-SDK-Client-IApiClient-GET'></a>
### GET `property`

##### Summary

Gets the IGET.

<a name='P-RecordPoint-Connectors-SDK-Client-IApiClient-POST'></a>
### POST `property`

##### Summary

Gets the IPOST.

<a name='P-RecordPoint-Connectors-SDK-Client-IApiClient-PUT'></a>
### PUT `property`

##### Summary

Gets the IPUT.

<a name='P-RecordPoint-Connectors-SDK-Client-IApiClient-SerializationSettings'></a>
### SerializationSettings `property`

##### Summary

Gets or sets json serialization settings.

<a name='M-RecordPoint-Connectors-SDK-Client-IApiClient-ApiBinariesPostWithHttpMessagesAndStreamAsync-System-String,System-String,System-String,System-String,System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-IO-Stream,System-Threading-CancellationToken-'></a>
### ApiBinariesPostWithHttpMessagesAndStreamAsync(connectorId,itemExternalId,binaryExternalId,fileName,correlationId,acceptLanguage,customHeaders,inputStream,cancellationToken) `method`

##### Summary

Post a binary to the Records365 Connector API with a stream.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| itemExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| binaryExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| correlationId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') |  |
| inputStream | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-IApiClientFactory'></a>
## IApiClientFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-IApiClientFactory-CreateApiClient-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings-'></a>
### CreateApiClient(settings) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| settings | [RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-IApiClientFactory-CreateAuthenticationProvider-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-'></a>
### CreateAuthenticationProvider(settings) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| settings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-IAuthenticationProvider'></a>
## IAuthenticationProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-IAuthenticationProvider-AcquireTokenAsync-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-'></a>
### AcquireTokenAsync(settings) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| settings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-IGET'></a>
## IGET `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

GET operations.

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiAggregationsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,connectorId,pageSize,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of aggregations that match a single metadata
field value for multi Tenanted Connectors

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| pageSize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiAggregationsfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,pagesize,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of aggregations that match a single metadata
field value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| pagesize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync-System-Nullable{System-Guid},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a connector configure model via connector ID MultiTenanted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiConnectorConfigurationsWithHttpMessagesAsync-System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsWithHttpMessagesAsync(acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Get all connectors for the given clientId across all tenants

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiConnectorConfigurationsidWithHttpMessagesAsync-System-Guid,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiConnectorConfigurationsidWithHttpMessagesAsync(id,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a connector configure model by its connector ID.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Guid},System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiItemsMultiTenantedfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,connectorId,pageSize,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of items that match a single metadata field value
for MutiTenanted Environment

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| connectorId | [System.Nullable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Guid}') |  |
| pageSize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiItemsfieldNamefieldValueWithHttpMessagesAsync-System-String,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiItemsfieldNamefieldValueWithHttpMessagesAsync(fieldName,fieldValue,pagesize,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of items that match a single metadata field
value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fieldName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the metadata field. |
| fieldValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The required value of the metadata field. |
| pagesize | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | The maximum number of items to return. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiNotificationsWithHttpMessagesAsync-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiNotificationsWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of notifications that are awaiting processing and
acknowledgement by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Connector ID for multi tenanted connections |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-IGET-ApiNotificationsconnectorIdWithHttpMessagesAsync-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiNotificationsconnectorIdWithHttpMessagesAsync(connectorId,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Gets a collection of notifications that are awaiting processing and
acknowledgement by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The query information |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |

<a name='T-RecordPoint-Connectors-SDK-Client-IPOST'></a>
## IPOST `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

POST operations.

<a name='M-RecordPoint-Connectors-SDK-Client-IPOST-ApiAggregationsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiAggregationsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Submits aggregations to Records365 vNext.
All aggregations are to be submitted to this endpoint.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel') | The aggregation to be managed. |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-IPOST-ApiBinariesGetSASTokenWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiBinariesGetSASTokenWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Get SAS Token for blob Resource

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-IPOST-ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Notifies Records365 that a new Binary has been uploaded

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') | The binary metadata information |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-IPOST-ApiBinariesWithHttpMessagesAsync-System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiBinariesWithHttpMessagesAsync(connectorId,itemExternalId,binaryExternalId,fileName,location,correlationId,isOldVersion,skipEnrichment,itemSourceLastModifiedDate,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Submits a binary to be archived and protected by Records365 vNext.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| itemExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| binaryExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| location | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| correlationId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| isOldVersion | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') |  |
| skipEnrichment | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') |  |
| itemSourceLastModifiedDate | [System.Nullable{System.DateTime}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTime}') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-IPOST-ApiItemsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiItemsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Submits an item to be managed by Records365 vNext.
All records are to be submitted to this endpoint.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel') | The item to be managed. |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-IPOST-ApiNotificationsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiNotificationsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Acknowledges a notification as having been processed by the
connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel') | The acknowledgement information. |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='T-RecordPoint-Connectors-SDK-Client-IPUT'></a>
## IPUT `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

PUT operations.

<a name='M-RecordPoint-Connectors-SDK-Client-IPUT-ApiAuditEventsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiAuditEventsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Submits a content source event to the Records365 vNext audit log.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel') | Audit event. |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | The headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-ItemSubmitContext'></a>
## ItemSubmitContext `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

An extended version of the [SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') class including list of binaries 
information sent via the Binary submission pipeline

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-ItemSubmitContext-BinariesSubmitted'></a>
### BinariesSubmitted `property`

##### Summary

List of binaries sent to binary pipeline and linked to this item

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-ItemUnspecifiedFieldValuePipelineElement'></a>
## ItemUnspecifiedFieldValuePipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A submit pipeline element that ensures that required fields for items have a value of "Unspecified".

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-ItemUnspecifiedFieldValuePipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new ItemUnspecifiedFieldValuePipelineElement with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-ItemUnspecifiedFieldValuePipelineElement-GetRequiredStringFields'></a>
### GetRequiredStringFields() `method`

##### Summary

Specifies the fields that are required for items.

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Helpers-LockHelper'></a>
## LockHelper `type`

##### Namespace

RecordPoint.Connectors.SDK.Helpers

##### Summary

The lock helper.

<a name='M-RecordPoint-Connectors-SDK-Helpers-LockHelper-LockedCreateOrUpdate``1-System-Object@,``0@,System-Func{``0,System-Boolean},System-Func{``0,``0}-'></a>
### LockedCreateOrUpdate\`\`1(lockingObject,lockedObject,lockCondition,lockedObjectFactory) `method`

##### Summary

Assists with writing code that requires a lock on an object
Generates a new value for the lockedObject based on its current value
Checks the input condition. If the input condition evaluates to true, attains a lock
and checks the input condition again. If the input condition still evaluates to true,
generates a new value for the locked object using the input factory (Which accepts the current
value of the locked object as a parameter for convenience)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| lockingObject | [System.Object@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object@ 'System.Object@') |  |
| lockedObject | [\`\`0@](#T-``0@ '``0@') |  |
| lockCondition | [System.Func{\`\`0,System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,System.Boolean}') |  |
| lockedObjectFactory | [System.Func{\`\`0,\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0}') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-RecordPoint-Connectors-SDK-Helpers-LockHelper-LockedGetOrCreate``1-System-Object@,``0@,System-Func{``0}-'></a>
### LockedGetOrCreate\`\`1(lockingObject,lockedObject,lockedObjectFactory) `method`

##### Summary

Assists with writing code that requires a double-checked lock 
https://en.wikipedia.org/wiki/Double-checked_locking
Gets the item's current value if it is not null. If it is null, locks using
the input locking object and checks if it is still null (For instances where multiple
threads have evaluated the object as being null) then populates it using the input factory

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| lockingObject | [System.Object@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object@ 'System.Object@') |  |
| lockedObject | [\`\`0@](#T-``0@ '``0@') |  |
| lockedObjectFactory | [System.Func{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0}') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-RecordPoint-Connectors-SDK-Helpers-LockHelper-LockedRead``1-System-Object@,``0@-'></a>
### LockedRead\`\`1(lockingObject,lockedObject) `method`

##### Summary

Locked the read.

##### Returns

A `T`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| lockingObject | [System.Object@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object@ 'System.Object@') | The locking object. |
| lockedObject | [\`\`0@](#T-``0@ '``0@') | The locked object. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='T-RecordPoint-Connectors-SDK-Filters-MatchResult'></a>
## MatchResult `type`

##### Namespace

RecordPoint.Connectors.SDK.Filters

##### Summary

The match result.

<a name='P-RecordPoint-Connectors-SDK-Filters-MatchResult-MatchReason'></a>
### MatchReason `property`

##### Summary

Gets or sets the match reason.

<a name='P-RecordPoint-Connectors-SDK-Filters-MatchResult-Result'></a>
### Result `property`

##### Summary

Gets or sets  a value indicating whether to result.

<a name='T-RecordPoint-Connectors-SDK-Client-NotSpecifiedCredentials'></a>
## NotSpecifiedCredentials `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

It is being used by AutoRest to handle Credentials, So this type of Credentials means there is actually no authentication set
on ApiClient level (it's being set at request level).
[ApiClientFactory](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactory 'RecordPoint.Connectors.SDK.Client.ApiClientFactory')

<a name='M-RecordPoint-Connectors-SDK-Client-NotSpecifiedCredentials-InnerProcessHttpRequestAsync-System-Net-Http-HttpRequestMessage,System-Threading-CancellationToken-'></a>
### InnerProcessHttpRequestAsync(request,cancellationToken) `method`

##### Summary

Inners process http request asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [System.Net.Http.HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') | The request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-NotSpecifiedCredentials-ProcessHttpRequestAsync-System-Net-Http-HttpRequestMessage,System-Threading-CancellationToken-'></a>
### ProcessHttpRequestAsync(request,cancellationToken) `method`

##### Summary

Processes http request asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [System.Net.Http.HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') | The request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-NotificationPullManager'></a>
## NotificationPullManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Manages pulling and acknowledging of connector notification messages.
Note this class only applies to connector types that use the "pull" notification method.

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationPullManager-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates a new NotificationPullManager.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationPullManager-AcknowledgeNotification-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Threading-CancellationToken-'></a>
### AcknowledgeNotification(factorySettings,authenticationSettings,acknowledgement,cancellationToken) `method`

##### Summary

Acknowledges a notification as having been processed.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| factorySettings | [RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings') |  |
| authenticationSettings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |
| acknowledgement | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationPullManager-GetAllPendingConnectorNotifications-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings,System-String,System-Threading-CancellationToken-'></a>
### GetAllPendingConnectorNotifications(factorySettings,authenticationSettings,connectorConfigId,cancellationToken) `method`

##### Summary

Queries for all connector notifications for a given connector instance that are pending acknowledgement.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| factorySettings | [RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings') |  |
| authenticationSettings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |
| connectorConfigId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-NotificationType'></a>
## NotificationType `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Defines constant values that may appear in the ConnectorNotificationModel.NotificationType field.
Possible values include: 'ItemDestroyed', 'Ping', 'ConnectorConfigCreated', 'ConnectorConfigUpdated', and 'ConnectorConfigDeleted'

<a name='F-RecordPoint-Connectors-SDK-Client-NotificationType-ConnectorConfigCreated'></a>
### ConnectorConfigCreated `constants`

##### Summary

The ConnectorConfigCreated Notification Type.
This notification is sent by Records365 vNext when a new instance of the
connector is created in the platform.

<a name='F-RecordPoint-Connectors-SDK-Client-NotificationType-ConnectorConfigDeleted'></a>
### ConnectorConfigDeleted `constants`

##### Summary

The ConnectorConfigDeleted Notification Type.
This notification is sent by Records365 vNext when an instance of the 
connector is deleted in the platform.

<a name='F-RecordPoint-Connectors-SDK-Client-NotificationType-ConnectorConfigUpdated'></a>
### ConnectorConfigUpdated `constants`

##### Summary

The ConnectorConfigUpdated Notification Type.
This notification is sent by Records365 vNext when an existing instance of
the connector is updated in the platform. Possible updates may include
configuration changes, or the connector being enabled or disabled by a user.

<a name='F-RecordPoint-Connectors-SDK-Client-NotificationType-ItemDestroyed'></a>
### ItemDestroyed `constants`

##### Summary

The ItemDestroyed Notification Type. 
This notification is sent by Records365 vNext when an item is disposed
in the platform.
The connector must permanently destroy all metadata, binaries and ay other 
information associated with the item in the content source.

<a name='F-RecordPoint-Connectors-SDK-Client-NotificationType-Ping'></a>
### Ping `constants`

##### Summary

The Ping Notification Type.
Used for testing purposes only.

<a name='T-RecordPoint-Connectors-SDK-Client-POST'></a>
## POST `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

POST operations.

<a name='M-RecordPoint-Connectors-SDK-Client-POST-#ctor-RecordPoint-Connectors-SDK-Client-ApiClient-'></a>
### #ctor(client) `constructor`

##### Summary

Initializes a new instance of the POST class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [RecordPoint.Connectors.SDK.Client.ApiClient](#T-RecordPoint-Connectors-SDK-Client-ApiClient 'RecordPoint.Connectors.SDK.Client.ApiClient') | Reference to the service client. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='P-RecordPoint-Connectors-SDK-Client-POST-Client'></a>
### Client `property`

##### Summary

Gets a reference to the ApiClient

<a name='M-RecordPoint-Connectors-SDK-Client-POST-ApiAggregationsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiAggregationsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Submits aggregations to Records365 vNext.
All aggregations are to be submitted to this endpoint.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel') | The aggregation to be managed. |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-POST-ApiBinariesGetSASTokenWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiBinariesGetSASTokenWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Get SAS Token for blob Resource

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-POST-ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Notifies Records365 that a new Binary has been uploaded

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') | The binary metadata information |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-POST-ApiBinariesWithHttpMessagesAsync-System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime},System-String,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiBinariesWithHttpMessagesAsync(connectorId,itemExternalId,binaryExternalId,fileName,location,correlationId,isOldVersion,skipEnrichment,itemSourceLastModifiedDate,acceptLanguage,customHeaders,cancellationToken) `method`

##### Summary

Submits a binary to be archived and protected by Records365 vNext.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| itemExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| binaryExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| location | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| correlationId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| isOldVersion | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') |  |
| skipEnrichment | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') |  |
| itemSourceLastModifiedDate | [System.Nullable{System.DateTime}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTime}') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown when a required parameter is null |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='M-RecordPoint-Connectors-SDK-Client-POST-ApiItemsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiItemsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Submits an item to be managed by Records365 vNext.
All records are to be submitted to this endpoint.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel') | The item to be managed. |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='M-RecordPoint-Connectors-SDK-Client-POST-ApiNotificationsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiNotificationsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Acknowledges a notification as having been processed by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel') | The acknowledgement information. |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='T-RecordPoint-Connectors-SDK-Client-POSTExtensions'></a>
## POSTExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Extension methods for POST.

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiAggregations-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-'></a>
### ApiAggregations(operations,acceptLanguage,body) `method`

##### Summary

Submits aggregations to Records365 vNext.
All aggregations are to be submitted to this endpoint.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel') | The aggregation to be managed. |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiAggregationsAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel,System-Threading-CancellationToken-'></a>
### ApiAggregationsAsync(operations,acceptLanguage,body,cancellationToken) `method`

##### Summary

Submits aggregations to Records365 vNext.
All aggregations are to be submitted to this endpoint.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel') | The aggregation to be managed. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinaries-RecordPoint-Connectors-SDK-Client-IPOST,System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime},System-String-'></a>
### ApiBinaries(operations,connectorId,itemExternalId,binaryExternalId,fileName,location,correlationId,isOldVersion,skipEnrichment,itemSourceLastModifiedDate,acceptLanguage) `method`

##### Summary

Submits a binary to be archived and protected by Records365 vNext.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| itemExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| binaryExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| location | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| correlationId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| isOldVersion | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') |  |
| skipEnrichment | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') |  |
| itemSourceLastModifiedDate | [System.Nullable{System.DateTime}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTime}') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime},System-String,System-Threading-CancellationToken-'></a>
### ApiBinariesAsync(operations,connectorId,itemExternalId,binaryExternalId,fileName,location,correlationId,isOldVersion,skipEnrichment,itemSourceLastModifiedDate,acceptLanguage,cancellationToken) `method`

##### Summary

Submits a binary to be archived and protected by Records365 vNext.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| itemExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| binaryExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| location | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| correlationId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| isOldVersion | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') |  |
| skipEnrichment | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') |  |
| itemSourceLastModifiedDate | [System.Nullable{System.DateTime}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTime}') |  |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesGetSASToken-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-'></a>
### ApiBinariesGetSASToken(operations,acceptLanguage,body) `method`

##### Summary

Get SAS Token for blob Resource

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesGetSASTokenAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Threading-CancellationToken-'></a>
### ApiBinariesGetSASTokenAsync(operations,acceptLanguage,body,cancellationToken) `method`

##### Summary

Get SAS Token for blob Resource

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesNotifyBinarySubmission-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-'></a>
### ApiBinariesNotifyBinarySubmission(operations,acceptLanguage,body) `method`

##### Summary

Notifies Records365 that a new Binary has been uploaded

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') | The binary metadata information |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiBinariesNotifyBinarySubmissionAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel,System-Threading-CancellationToken-'></a>
### ApiBinariesNotifyBinarySubmissionAsync(operations,acceptLanguage,body,cancellationToken) `method`

##### Summary

Notifies Records365 that a new Binary has been uploaded

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel') | The binary metadata information |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiItems-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-'></a>
### ApiItems(operations,acceptLanguage,body) `method`

##### Summary

Submits an item to be managed by Records365 vNext.
All records are to be submitted to this endpoint.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel') | The item to be managed. |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiItemsAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel,System-Threading-CancellationToken-'></a>
### ApiItemsAsync(operations,acceptLanguage,body,cancellationToken) `method`

##### Summary

Submits an item to be managed by Records365 vNext.
All records are to be submitted to this endpoint.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel') | The item to be managed. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiNotifications-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-'></a>
### ApiNotifications(operations,acceptLanguage,body) `method`

##### Summary

Acknowledges a notification as having been processed by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel') | The acknowledgement information. |

<a name='M-RecordPoint-Connectors-SDK-Client-POSTExtensions-ApiNotificationsAsync-RecordPoint-Connectors-SDK-Client-IPOST,System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Threading-CancellationToken-'></a>
### ApiNotificationsAsync(operations,acceptLanguage,body,cancellationToken) `method`

##### Summary

Acknowledges a notification as having been processed by the connector.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPOST](#T-RecordPoint-Connectors-SDK-Client-IPOST 'RecordPoint.Connectors.SDK.Client.IPOST') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel') | The acknowledgement information. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Client-PUT'></a>
## PUT `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

PUT operations.

<a name='M-RecordPoint-Connectors-SDK-Client-PUT-#ctor-RecordPoint-Connectors-SDK-Client-ApiClient-'></a>
### #ctor(client) `constructor`

##### Summary

Initializes a new instance of the PUT class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [RecordPoint.Connectors.SDK.Client.ApiClient](#T-RecordPoint-Connectors-SDK-Client-ApiClient 'RecordPoint.Connectors.SDK.Client.ApiClient') | Reference to the service client. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown when a required parameter is null |

<a name='P-RecordPoint-Connectors-SDK-Client-PUT-Client'></a>
### Client `property`

##### Summary

Gets a reference to the ApiClient

<a name='M-RecordPoint-Connectors-SDK-Client-PUT-ApiAuditEventsWithHttpMessagesAsync-System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel,System-Collections-Generic-Dictionary{System-String,System-Collections-Generic-List{System-String}},System-Threading-CancellationToken-'></a>
### ApiAuditEventsWithHttpMessagesAsync(acceptLanguage,body,customHeaders,cancellationToken) `method`

##### Summary

Submits a content source event to the Records365 vNext audit log.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel') | Audit event. |
| customHeaders | [System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.List{System.String}}') | Headers that will be added to request. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.HttpOperationException](#T-Microsoft-Rest-HttpOperationException 'Microsoft.Rest.HttpOperationException') | Thrown when the operation returned an invalid status code |
| [Microsoft.Rest.SerializationException](#T-Microsoft-Rest-SerializationException 'Microsoft.Rest.SerializationException') | Thrown when unable to deserialize the response |

<a name='T-RecordPoint-Connectors-SDK-Client-PUTExtensions'></a>
## PUTExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Extension methods for PUT.

<a name='M-RecordPoint-Connectors-SDK-Client-PUTExtensions-ApiAuditEvents-RecordPoint-Connectors-SDK-Client-IPUT,System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-'></a>
### ApiAuditEvents(operations,acceptLanguage,body) `method`

##### Summary

Submits a content source event to the Records365 vNext audit log.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPUT](#T-RecordPoint-Connectors-SDK-Client-IPUT 'RecordPoint.Connectors.SDK.Client.IPUT') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel') | Audit event. |

<a name='M-RecordPoint-Connectors-SDK-Client-PUTExtensions-ApiAuditEventsAsync-RecordPoint-Connectors-SDK-Client-IPUT,System-String,RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel,System-Threading-CancellationToken-'></a>
### ApiAuditEventsAsync(operations,acceptLanguage,body,cancellationToken) `method`

##### Summary

Submits a content source event to the Records365 vNext audit log.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operations | [RecordPoint.Connectors.SDK.Client.IPUT](#T-RecordPoint-Connectors-SDK-Client-IPUT 'RecordPoint.Connectors.SDK.Client.IPUT') | The operations group for this extension method. |
| acceptLanguage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| body | [RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel') | Audit event. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-PipelineSelectorPipelineElement'></a>
## PipelineSelectorPipelineElement `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A submit pipeline element that branches the pipeline based on whether the submission is
for an item or an aggregation. The pipeline element decides which branch to take based on the 
ItemTypeId field in the core metadata. A value of "1" indicates that the item is an aggregation,
and the aggregation branch is used. Other values use the item branch.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-PipelineSelectorPipelineElement-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(submitRecord,submitAggregation) `constructor`

##### Summary

Constructs a new PipelineSelectorPipelineElement.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitRecord | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |
| submitAggregation | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-PipelineSelectorPipelineElement-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase'></a>
## SubmitPipelineElementBase `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

Base class for submit pipeline elements.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new SubmitPipelineElementBase with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-Log'></a>
### Log `property`

##### Summary

A log.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-InvokeNext-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### InvokeNext(submitContext) `method`

##### Summary

Invokes the next element in the submission pipeline, if one exists.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-LogMessage-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String,System-String-'></a>
### LogMessage(context,methodName,message) `method`

##### Summary

Logs a message, providing information from the SubmitContext.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-LogVerbose-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String,System-String-'></a>
### LogVerbose(context,methodName,message) `method`

##### Summary

Logs a verbose message, providing information from the SubmitContext.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-LogWarning-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String,System-String-'></a>
### LogWarning(context,methodName,message) `method`

##### Summary

Logs a warning message, providing information from the SubmitContext.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-SkipNext-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext,System-String-'></a>
### SkipNext(submitContext,reason) `method`

##### Summary

Indicates that this pipeline element is terminating the pipeline.
Note this method only performs the appropriate logging and sets the SubmitContext.SubmitResult to Skipped.
The calling method still needs to be careful to return early or otherwise skip the rest of the pipeline.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitPipelineElementBase-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary

Implement in a derived class to provide custom submit pipeline functionality.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-UnspecifiedFieldValuePipelineElementBase'></a>
## UnspecifiedFieldValuePipelineElementBase `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

Ensures that a value is provided for all fields required by the Connector API.
If a null or empty value is provided for a required field, this pipeline element
substitutes the value "Unspecified".
Doesn't substitute for ConnectorId, ExternalId or ParentExternalId.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-UnspecifiedFieldValuePipelineElementBase-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(next) `constructor`

##### Summary

Constructs a new UnspecifiedFieldValuePipelineElementBase with an optional next submit
pipeline element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') |  |

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-UnspecifiedFieldValuePipelineElementBase-GetRequiredStringFields'></a>
### GetRequiredStringFields() `method`

##### Summary

Implement in a derived class to specify the names of any string fields that are required.
The pipeline element will substitute any blank or missing values with "Unspecified" for these
fields.

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-UnspecifiedFieldValuePipelineElementBase-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary

Checks core metadata for any required fields that are blank or missing
and provides the value "Unspecified" for those fields.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |
