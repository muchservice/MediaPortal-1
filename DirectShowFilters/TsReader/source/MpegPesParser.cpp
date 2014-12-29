/* 
 *	Copyright (C) 2006 Team MediaPortal
 *	http://www.team-mediaportal.com
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */

#pragma warning(disable:4996)
#pragma warning(disable:4995)
#include "StdAfx.h"

#include <winsock2.h>
#include <ws2tcpip.h>

#include <commdlg.h>
#include <bdatypes.h>
#include <time.h>
#include <streams.h>
#include <initguid.h>

#include "MpegPesParser.h"

// For more details for memory leak detection see the alloctracing.h header
#include "..\..\alloctracing.h"

extern void LogDebug(const char *fmt, ...) ;

CMpegPesParser::CMpegPesParser()
{
	pmt=CMediaType();
	pmt.InitMediaType();
	pmt.bFixedSizeSamples=false;
	
	audPmt=CMediaType();
	audPmt.InitMediaType();
	audPmt.bFixedSizeSamples=false;
	
	basicVideoInfo=BasicVideoInfo();
	basicAudioInfo=BasicAudioInfo();
}

bool CMpegPesParser::ParseVideo(byte* tsPacket,bool isMpeg2,bool reset)
{
	bool parsed=false;
  __int64 framesize=hdrParser.GetSize();

	if (isMpeg2)
	{
		seqhdr seq;
		if (hdrParser.Read(seq,framesize,&pmt,reset))
		{
			//hdrParser.DumpSequenceHeader(seq);
			basicVideoInfo.width=seq.width;
			basicVideoInfo.height=seq.height;
			basicVideoInfo.fps=10000000.0 / (double)seq.ifps;
			basicVideoInfo.arx=seq.arx;
			basicVideoInfo.ary=seq.ary;
			if (seq.progressive==0)
				basicVideoInfo.isInterlaced=1;
			else
				basicVideoInfo.isInterlaced=0;
			basicVideoInfo.streamType=1; //MPEG2
			basicVideoInfo.isValid=true;
			parsed=true;
		}
	}
	else 
	{
	  // avchdr avc;
		if (hdrParser.Read(avc,framesize,&pmt,reset))
		{
			//hdrParser.DumpAvcHeader(avc);
			basicVideoInfo.width=avc.width;
			basicVideoInfo.height=avc.height;
			basicVideoInfo.fps=10000000.0 / (double)avc.AvgTimePerFrame;
			basicVideoInfo.arx=avc.arx;
			basicVideoInfo.ary=avc.ary;
			if (!avc.progressive)
				basicVideoInfo.isInterlaced=1;
			else
				basicVideoInfo.isInterlaced=0;
			basicVideoInfo.streamType=2; // H264
			basicVideoInfo.isValid=true;
			parsed=true;
		}
	}
	return parsed;
}

bool CMpegPesParser::OnTsPacket(byte *Frame,int Length,bool isMpeg2,bool reset)
{
	//LogDebug("Framesize: %i",Length);
	//if (Length<=100) return false ; // arbitrary for safety.
  CAutoLock lock (&m_sectionVideoPmt);
	hdrParser.Reset(Frame,Length);
	return ParseVideo(Frame,isMpeg2,reset);
}

void CMpegPesParser::VideoReset()
{
  CAutoLock lock (&m_sectionVideoPmt);

	basicVideoInfo.width=0;
	basicVideoInfo.height=0;
	basicVideoInfo.fps=0;
	basicVideoInfo.arx=0;
	basicVideoInfo.ary=0;
	basicVideoInfo.isInterlaced=0;
	basicVideoInfo.streamType=0;
	basicVideoInfo.isValid=false;
}

bool CMpegPesParser::ParseAudio(byte* audioPacket,bool reset)
{
	bool parsed=false;
	__int64 framesize=hdrParser.GetSize();
  aachdr aac;
	if (hdrParser.Read(aac,framesize,&audPmt))
	{
    basicAudioInfo.sampleRate=aac.nSamplesPerSec;
    basicAudioInfo.channels=aac.channels;    
    basicAudioInfo.aacObjectType=aac.profile+1;
    basicAudioInfo.streamType = 3;
	  basicAudioInfo.pmtValid = true;	
    basicAudioInfo.isValid = true;
	  parsed=true;
	}
	return parsed;
}

bool CMpegPesParser::OnAudioPacket(byte *Frame, int Length, bool reset)
{
  CAutoLock lock (&m_sectionAudioPmt);
	hdrParser.Reset(Frame,Length);
	return ParseAudio(Frame,reset);
}

void CMpegPesParser::AudioReset()
{
  CAutoLock lock (&m_sectionAudioPmt);

	basicAudioInfo.isValid=false;	
	basicAudioInfo.sampleRate=0;
	basicAudioInfo.channels=0;
	basicAudioInfo.streamType=0;
	basicAudioInfo.pmtValid=false;	
}

void CMpegPesParser::AudioValidReset()
{
  CAutoLock lock (&m_sectionAudioPmt);
	basicAudioInfo.isValid=false;	
}
