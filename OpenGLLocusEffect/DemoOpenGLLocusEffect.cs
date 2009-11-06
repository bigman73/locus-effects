#region © Copyright 2005, 2009 Yuval Naveh, Locus Effects. LGPL.
/* Locus Effects
 
    © Copyright 2005, 2009, Yuval Naveh.
     All rights reserved.
 
    This file is part of Locus Effects.

    Locus Effects is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Locus Effects is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser Public License for more details.

    You should have received a copy of the GNU Lesser Public License
    along with Locus Effects.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace BigMansStuff.LocusEffects.OpenGL
{
    /// <summary>
    /// Demonstration of a LocusEffect using OpenGL
    /// The Demo shows a rolling pyramid
    /// </summary>
    public class DemoOpenGLLocusEffect: OpenGLLocusEffect
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DemoOpenGLLocusEffect"/> class.
        /// </summary>
        public DemoOpenGLLocusEffect()
        {
            Name = "DemoOpenGLLocusEffect";
        }

        #endregion

        #region OpenGL Rendering

        /// <summary>
        /// Renders the open GL scene.
        /// </summary>
        public override void RenderOpenGLScene()
        {
            base.RenderOpenGLScene();

            m_xAngle += 12.0f;
            m_yAngle += 6.0f;
            m_zAngle += 2.0f;

            byte R1 = 255;
            byte G1 = 0;
            byte B1 = 0;
            byte R2 = 255;
            byte G2 = 255;
            byte B2 = 0;

            Gl.glTranslatef( 0.5f, 0.0f, -7.0f - m_xAngle / 20 );

            Gl.glRotated( m_xAngle, 1.0, 0.0, 0.0 );
            Gl.glRotated( m_yAngle, 0.0, 1.0, 0.0 );
            Gl.glRotated( m_zAngle, 0.0, 0.0, 1.0 );

            //Up
            Gl.glBegin( Gl.GL_TRIANGLE_FAN );
                Gl.glColor3ub( R2, G2, B2 ); Gl.glVertex3d( 0.0, 1.414, 0.0 );
                Gl.glColor3ub( R1, G1, B1 ); Gl.glVertex3d( 1.0, 0.0, 1.0 );
                Gl.glColor3ub( 0, 0, 0 ); Gl.glVertex3d( 1.0, 0.0, -1.0 );

                Gl.glColor3ub( R1, G1, B1 ); Gl.glVertex3d( -1.0, 0.0, -1.0 );
                Gl.glColor3ub( 0, 0, 0 ); Gl.glVertex3d( -1.0, 0.0, 1.0 );
                Gl.glColor3ub( R1, G1, B1 ); Gl.glVertex3d( 1.0, 0.0, 1.0 );
            Gl.glEnd();

            //Down
            Gl.glBegin( Gl.GL_TRIANGLE_FAN );
            Gl.glColor3ub( R2, G2, B2 ); Gl.glVertex3d( 0.0, -1.414, 0.0 );
                Gl.glColor3ub( R1, G1, B1 ); Gl.glVertex3d( 1.0, 0.0, 1.0 );
                Gl.glColor3ub( 64, 64, 64 ); Gl.glVertex3d( -1.0, 0.0, 1.0 );

                Gl.glColor3ub( R1, G1, B1 ); Gl.glVertex3d( -1.0, 0.0, -1.0 );
                Gl.glColor3ub( 64, 64, 64 ); Gl.glVertex3d( 1.0, 0.0, -1.0 );
                Gl.glColor3ub( R1, G1, B1 ); Gl.glVertex3d( 1.0, 0.0, 1.0 );
            Gl.glEnd();
        }

        /// <summary>
        /// All setup for OpenGL goes here.
        /// </summary>
        protected override void InitGLScene()
        {
            base.InitGLScene();
            
            m_xAngle = 0;
            m_yAngle = 0;
            m_zAngle = 0;

            m_runTimeData.Opacity = 100;
        }

        #endregion

        #region Private Members

        private float m_xAngle;
        private float m_yAngle;
        private float m_zAngle;

        #endregion
    }
}
