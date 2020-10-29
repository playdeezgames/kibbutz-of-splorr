namespace Splorr.Kibbutz

open Splorr.Common

module Output =
    type WriteSink = string -> unit
    type WriteContext =
        abstract member writeSink : WriteSink ref
    let internal Write
            (context : CommonContext) =
        (context :?> WriteContext).writeSink.Value

