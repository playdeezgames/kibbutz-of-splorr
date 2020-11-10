namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System


module SessionIdentifierStore =
    let GenerateIdentifier() : SessionIdentifier =
        Guid.NewGuid()
