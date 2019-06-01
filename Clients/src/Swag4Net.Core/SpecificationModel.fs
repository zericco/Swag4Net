namespace Swag4Net.Core

open System.Net

module SpecificationModel =
  type TypeName = string
  type Documentation =
    { Infos:Infos
      Host:string
      BasePath:string
      Schemes:string list
      Routes:Route list
      ExternalDocs:Map<string,string>
      Definitions:Schema list }
  and Infos =
    { Description:string
      Version:string
      Title:string
      TermsOfService:string
      Contact:Contact option
      License:License option }
  and Contact = 
    | Email of string
  and License = 
    { Name:string; Url:string }
  and Schema = 
    { Name:string
      Properties:Property list }
  and Property = 
    { Name:string; Type:DataTypeDescription; Enums:string list option }

  and DataTypeDescription = 
    | PrimaryType of DataType
    | ComplexType of Schema
    member __.IsArray() = 
      match __ with 
      | PrimaryType d ->
          match d with 
          | DataType.Array _ -> true
          | _ -> false
      | ComplexType _ -> false

  and [<RequireQualifiedAccess>] DataType = 
    | String of StringFormat option
    | Number
    | Integer
    | Integer64
    | Boolean
    | Array of DataTypeDescription
    | Object
  and [<RequireQualifiedAccess>] StringFormat =
    | Date
    | DateTime
    | Password
    | Base64Encoded
    | Binary
  and Route = 
    { Path:string
      Verb:string
      Tags:string list
      Summary:string
      Description:string
      OperationId:string
      Consumes:string list
      Produces:string list
      Parameters:Parameter list
      Responses:Response list }
  and ParameterLocation =
    | InQuery
    | InHeader
    | InPath
    | InCookie
    | InBody
    | InFormData
  and Parameter =
    { Location:ParameterLocation
      Name:string
      Description:string
      Deprecated:bool
      AllowEmptyValue:bool
      ParamType:DataTypeDescription
      Required:bool }
  and Response = 
    { Code:StatusCodeInfo
      Description:string
      Type:DataTypeDescription option }
  and StatusCodeInfo =
    | AnyStatusCode
    | StatusCode of HttpStatusCode