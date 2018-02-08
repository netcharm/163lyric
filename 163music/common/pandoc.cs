using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netcharm.common
{
    public class Pandoc : ConsoleApp
    {
        //PANDOCOPT =
        //-S 
        //--include-in-header="%INCLUDE%" 
        //--include-before-body="%BODY_BEFORE%" 
        //--include-after-body="%BODY_AFTER%" 
        //--columns=100 
        //--toc 
        //-t html5
        //--section-divs

        public bool Smart = false;
        public string IncludeInHeader = "";
        public string IncludeBeforeBody = "";
        public string IncludeAfterBody = "";
        public uint Columns = 100;
        public bool TOC = true;
        public string FromFormat = "";
        public string ToFormat = "";
        public string HighlightSyle = "";
        public string CSS = "";
        public string MoreOptions = "";
        public string InputFile = "";
        public string OutputFile = "";
        public bool SectionDivs=false;

        public int Run( string[] args )
        {
            List<string> param = new List<string>();

            if ( Smart ) param.Add( "-S" );

            if ( !string.IsNullOrEmpty( IncludeInHeader ) )
                param.Add( $"--include-in-header=\"{IncludeInHeader}\"" );
            if ( !string.IsNullOrEmpty( IncludeBeforeBody ) )
                param.Add( $"--include-before-body=\"{IncludeBeforeBody}\"" );
            if ( !string.IsNullOrEmpty( IncludeAfterBody ) )
                param.Add( $"--include-after-body=\"{IncludeAfterBody}\"" );

            if ( !string.IsNullOrEmpty( MoreOptions ) )
                param.Add( MoreOptions );

            if ( TOC ) param.Add( "--toc" );

            if ( SectionDivs ) param.Add( "--section-divs" );

            if ( Columns > 0 ) param.Add( $"--columns={Columns}" );

            if ( !string.IsNullOrEmpty( FromFormat ) )
                param.Add( $"-f {FromFormat}" );

            if ( !string.IsNullOrEmpty( ToFormat ) )
                param.Add( $"-t {ToFormat}" );

            if ( !string.IsNullOrEmpty( InputFile ) )
                param.Add( $"-o \"{OutputFile}\"" );

            if ( !string.IsNullOrEmpty( InputFile ) )
                param.Add( $"\"{InputFile}\"" );

            param.AddRange( args );

            return ( Run( "pandoc", param.ToArray() ) );
        }
    }
}

