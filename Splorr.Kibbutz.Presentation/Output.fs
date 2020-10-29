namespace Splorr.Kibbutz.Presentation

open Splorr.Common

module Output =
    type WriteSink = Message -> unit
    type WriteContext =
        abstract member writeSink : WriteSink ref
    let internal Write
            (context : CommonContext) =
        (context :?> WriteContext).writeSink.Value

