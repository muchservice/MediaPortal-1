/* 
 *	Copyright (C) 2005 Team MediaPortal
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
using System;
using System.Drawing;
using Microsoft.DirectX;

using Microsoft.DirectX.Direct3D;
using Direct3D=Microsoft.DirectX.Direct3D;




namespace MediaPortal.GUI.Library
{
	/// <summary>
	/// This class will draw a placeholder for the current video window
	/// if no video is playing it will present an empty rectangle
	/// </summary>
	public class GUIVideoControl : GUIControl
	{
		GUIImage image;
		[XMLSkinElement("textureFocus")]	protected string	m_strImgFocusTexture="";
		[XMLSkinElement("action")]			protected int		m_iAction=-1;
		protected GUIImage FocusImage=null;
		
		protected Rectangle[] m_vidWindow= new Rectangle[1];
	
		public GUIVideoControl(int dwParentID) : base(dwParentID)
		{
		}
		public GUIVideoControl(int dwParentID, int dwControlId, int dwPosX, int dwPosY, int dwWidth, int dwHeight, string texturename)
			:base(dwParentID, dwControlId, dwPosX, dwPosY, dwWidth, dwHeight)
		{
			m_strImgFocusTexture = texturename;
			FinalizeConstruction();
		}
		public override void FinalizeConstruction()
		{
			base.FinalizeConstruction ();
			FocusImage = new GUIImage(m_dwParentID, m_dwControlID, m_dwPosX, m_dwPosY,m_dwWidth, m_dwHeight, m_strImgFocusTexture ,0);
			image = new GUIImage(m_dwParentID, m_dwControlID, m_dwPosX, m_dwPosY,m_dwWidth, m_dwHeight, "black.bmp" ,1);
		}


    public override void AllocResources()
    {
      base.AllocResources ();
      FocusImage.AllocResources();
			image.AllocResources();
    }
    public override void FreeResources()
    {
      base.FreeResources ();
      FocusImage.FreeResources();
			image.FreeResources();
    }


    public override bool CanFocus()
    {
      if (FocusImage.FileName==String.Empty) return false;
      return true;
    }


    public override void Render(float timePassed)
    {
      if (GUIGraphicsContext.EditMode==false)
      {
        if (!IsVisible) return;
      }
      
      float x=base.XPosition;
      float y=base.YPosition;
      GUIGraphicsContext.Correct(ref x,ref y);

      m_vidWindow[0].X=(int)x;
      m_vidWindow[0].Y=(int)y;
      m_vidWindow[0].Width=base.Width;
      m_vidWindow[0].Height=base.Height;
      if (!GUIGraphicsContext.Calibrating )
      {
        GUIGraphicsContext.VideoWindow=m_vidWindow[0];
				if (GUIGraphicsContext.ShowBackground)
				{
          if (Focus)
          {
            int xoff=5; int yoff=5;
            int w=10;int h=10;
            GUIGraphicsContext.ScalePosToScreenResolution(ref xoff, ref yoff);
            GUIGraphicsContext.ScalePosToScreenResolution(ref w, ref h);
            xoff += GUIGraphicsContext.OffsetX;
            yoff += GUIGraphicsContext.OffsetY;
            FocusImage.SetPosition((int)x-xoff,(int)y-yoff);
            FocusImage.Width=base.Width+w;
            FocusImage.Height=base.Height+h;
            FocusImage.Render(timePassed);
          }

					if (GUIGraphicsContext.graphics!=null)
          {
						GUIGraphicsContext.graphics.FillRectangle(Brushes.Black , m_vidWindow[0].X,m_vidWindow[0].Y,base.Width,base.Height);
					}
					else
					{
						//image.SetPosition(m_vidWindow[0].X,m_vidWindow[0].Y);
						//image.Width=m_vidWindow[0].Width;
						//image.Height=m_vidWindow[0].Height;
						image.Render(timePassed);
						//GUIGraphicsContext.DX9Device.Clear( ClearFlags.Target|ClearFlags.Target, Color.FromArgb(255,1,1,1), 1.0f, 0,m_vidWindow);
					}
				}
				else
				{
					if (GUIGraphicsContext.graphics!=null)
					{
						GUIGraphicsContext.graphics.FillRectangle(Brushes.Black , m_vidWindow[0].X,m_vidWindow[0].Y,base.Width,base.Height);
					}
				}
      }
    }

    public override void OnAction( Action action) 
    {
      base.OnAction(action);
      GUIMessage message ;
      if (Focus)
      {
        if (action.wID == Action.ActionType.ACTION_MOUSE_CLICK||action.wID == Action.ActionType.ACTION_SELECT_ITEM)
        {
          // If this button corresponds to an action generate that action.
          if (ActionID >=0)
          {
            Action newaction = new Action((Action.ActionType)ActionID,0,0);
            GUIGraphicsContext.OnAction(newaction);
            return;
          }
          
          message = new GUIMessage(GUIMessage.MessageType.GUI_MSG_CLICKED,WindowId,GetID, ParentID,0,0,null );
          GUIGraphicsContext.SendMessage(message);
        }
      }
    }

    /// <summary>
    /// Get/set the action ID that corresponds to this button.
    /// </summary>
    public int ActionID
    {
      get { return m_iAction;}
      set { m_iAction=value;}

    }

    /// <summary>
    /// Checks if the x and y coordinates correspond to the current control.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>True if the control was hit.</returns>
    public override bool HitTest(int x, int y,out int controlID, out bool focused)
    {
      focused=Focus;
      controlID=GetID;
      if (!IsVisible || Disabled || CanFocus() == false) 
      {
        return false;
      }
      if ( InControl(x,y, out controlID))
      {
        if (CanFocus())
        {
          return true; 
        }
      }
      focused=Focus=false;
      return false;
    }

    
	}
}
