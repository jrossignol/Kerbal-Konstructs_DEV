﻿using KerbalKonstructs.LaunchSites;
using KerbalKonstructs.SpaceCenters;
using KerbalKonstructs.StaticObjects;
using KerbalKonstructs.Utilities;
using System;
using System.Collections.Generic;
using KSP.UI.Screens;
using KerbalKonstructs.API;
using UnityEngine;

namespace KerbalKonstructs.UI
{
	public class MapIconManager
	{
		public Texture VABIcon = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/VABMapIcon", false);
		public Texture SPHIcon = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/SPHMapIcon", false);
		public Texture ANYIcon = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/ANYMapIcon", false);
		public Texture TrackingStationIcon = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/TrackingMapIcon", false);

		public Texture2D tNormalButton = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapButtonNormal", false);
		public Texture2D tHoverButton = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapButtonHover", false);

		public Texture tOpenBasesOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapOpenBasesOn", false);
		public Texture tOpenBasesOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapOpenBasesOff", false);
		public Texture tClosedBasesOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapClosedBasesOn", false);
		public Texture tClosedBasesOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapClosedBasesOff", false);
		public Texture tHelipadsOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapHelipadsOn", false);
		public Texture tHelipadsOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapHelipadsOff", false);
		public Texture tRunwaysOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapRunwaysOn", false);
		public Texture tRunwaysOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapRunwaysOff", false);
		public Texture tTrackingOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapTrackingOn", false);
		public Texture tTrackingOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapTrackingOff", false);
		public Texture tLaunchpadsOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapLaunchpadsOn", false);
		public Texture tLaunchpadsOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapLaunchpadsOff", false);
		public Texture tOtherOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapOtherOn", false);
		public Texture tOtherOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapOtherOff", false);
		public Texture tRadarCover = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/radarcover", false);
		public Texture tRadarOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapRadarOn", false);
		public Texture tRadarOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapRadarOff", false);
		public Texture tUplinksOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapUplinksOn", false);
		public Texture tUplinksOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapUplinksOff", false);
		public Texture tGroundCommsOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapGroundCommsOn", false);
		public Texture tGroundCommsOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapGroundCommsOff", false);
		public Texture tHideOn = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapHideOn", false);
		public Texture tHideOff = GameDatabase.Instance.GetTexture("KerbalKonstructs/Assets/mapHideOff", false);

		Rect mapManagerRect = new Rect(250, 40, 480, 75);

		private Boolean displayingTooltip = false;
		private Boolean displayingTooltip2 = false;
		
		static LaunchSite selectedSite = null;
		static StaticObject selectedFacility = null;

		public float iFundsOpen = 0;
		public float iFundsClose = 0;
		public Boolean isOpen = false;

		public float iFundsOpen2 = 0;
		public float iFundsClose2 = 0;
		public Boolean isOpen2 = false;
		public Boolean bChangeTargetType = false;
		public Boolean bChangeTarget = false;
		public Boolean showBaseManager = false;
		public Boolean bHideOccluded = false;
		public Boolean bHideOccluded2 = false;

		public Vector2 sitesScrollPosition;
		public Vector2 descriptionScrollPosition;

		Vector3 ObjectPos = new Vector3(0, 0, 0);

		bool loadedPersistence = false;

		GUIStyle Yellowtext;
		GUIStyle TextAreaNoBorder;
		GUIStyle BoxNoBorder;
		GUIStyle ButtonKK;
		GUIStyle ButtonRed;
		GUIStyle KKToolTip;

		GUIStyle navStyle = new GUIStyle();

		public MapIconManager()
		{
			navStyle.padding.left = 0;
			navStyle.padding.right = 0;
			navStyle.padding.top = 1;
			navStyle.padding.bottom = 3;
			navStyle.normal.background = null;
		}

		public static LaunchSite getSelectedSite()
		{
			LaunchSite thisSite = selectedSite;
			return thisSite;
		}

		public void displayMapIconToolTip(string sitename, Vector3 pos)
		{
			displayingTooltip = true;
			GUI.Label(new Rect((float)(pos.x) + 16, (float)(Screen.height - pos.y) - 8, 210, 25), sitename);
		}

		public void drawManager()
		{
			mapManagerRect = GUI.Window(0xB00B2E7, mapManagerRect, drawMapManagerWindow, "", navStyle);
		}

		void drawMapManagerWindow(int windowID)
		{
			ButtonRed = new GUIStyle(GUI.skin.button);
			ButtonRed.normal.textColor = Color.red;
			ButtonRed.active.textColor = Color.red;
			ButtonRed.focused.textColor = Color.red;
			ButtonRed.hover.textColor = Color.red;

			ButtonKK = new GUIStyle(GUI.skin.button);
			ButtonKK.padding.left = 0;
			ButtonKK.padding.right = 0;
			ButtonKK.normal.background = tNormalButton;
			ButtonKK.hover.background = tHoverButton;

			Yellowtext = new GUIStyle(GUI.skin.box);
			Yellowtext.normal.textColor = Color.yellow;
			Yellowtext.normal.background = null;

			TextAreaNoBorder = new GUIStyle(GUI.skin.textArea);
			TextAreaNoBorder.normal.background = null;

			BoxNoBorder = new GUIStyle(GUI.skin.box);
			BoxNoBorder.normal.background = null;

			KKToolTip = new GUIStyle(GUI.skin.box);
			KKToolTip.normal.textColor = Color.white;
			KKToolTip.fontSize = 11;
			KKToolTip.fontStyle = FontStyle.Normal;

			if (!loadedPersistence && MiscUtils.isCareerGame())
			{
				PersistenceFile<LaunchSite>.LoadList(LaunchSiteManager.AllLaunchSites, "LAUNCHSITES", "KK");
				foreach (StaticObject obj in KerbalKonstructs.instance.getStaticDB().getAllStatics())
				{
					if ((string)obj.getSetting("FacilityType") == "TrackingStation")
						PersistenceUtils.loadStaticPersistence(obj);
				}

				loadedPersistence = true;
			}

			GUILayout.BeginHorizontal();
			GUILayout.Box(" ", BoxNoBorder, GUILayout.Height(34));
			
			GUI.enabled = (MiscUtils.isCareerGame());
			if (!MiscUtils.isCareerGame())
			{
				GUILayout.Button(tOpenBasesOff, GUILayout.Width(32), GUILayout.Height(32));
				GUILayout.Button(tClosedBasesOff, GUILayout.Width(32), GUILayout.Height(32));
				GUILayout.Box(" ", BoxNoBorder, GUILayout.Height(34));
				GUILayout.Button(tTrackingOff, GUILayout.Width(32), GUILayout.Height(32));
			}
			else
			{
				if (KerbalKonstructs.instance.mapShowOpen)
				{
					if (GUILayout.Button(new GUIContent(tOpenBasesOn, "Opened"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowOpen = false;
				}
				else
				{
					if (GUILayout.Button(new GUIContent(tOpenBasesOff, "Opened"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowOpen = true;
				}

				if (!KerbalKonstructs.instance.disableDisplayClosed)
				{
					if (KerbalKonstructs.instance.mapShowClosed)
					{
						if (GUILayout.Button(new GUIContent(tClosedBasesOn, "Closed"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
							KerbalKonstructs.instance.mapShowClosed = false;
					}
					else
					{
						if (GUILayout.Button(new GUIContent(tClosedBasesOff, "Closed"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
							KerbalKonstructs.instance.mapShowClosed = true;
					}
				}

				GUILayout.Box(" ", BoxNoBorder, GUILayout.Height(34));
				if (KerbalKonstructs.instance.mapShowOpenT)
				{
					if (GUILayout.Button(new GUIContent(tTrackingOn, "Tracking Stations"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowOpenT = false;
				}
				else
				{

					if (GUILayout.Button(new GUIContent(tTrackingOff, "Tracking Stations"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowOpenT = true;
				}
			}
			GUI.enabled = true;

			GUILayout.Box(" ", BoxNoBorder, GUILayout.Height(34));

			if (KerbalKonstructs.instance.mapShowRocketbases)
			{
				if (GUILayout.Button(new GUIContent(tLaunchpadsOn, "Rocketpads"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapShowRocketbases = false;
			}
			else
			{
				if (GUILayout.Button(new GUIContent(tLaunchpadsOff, "Rocketpads"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapShowRocketbases = true;
			}

			if (KerbalKonstructs.instance.mapShowHelipads)
			{
				if (GUILayout.Button(new GUIContent(tHelipadsOn, "Helipads"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapShowHelipads = false;
			}
			else
			{
				if (GUILayout.Button(new GUIContent(tHelipadsOff, "Helipads"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapShowHelipads = true;
			}

			if (KerbalKonstructs.instance.mapShowRunways)
			{
				if (GUILayout.Button(new GUIContent(tRunwaysOn, "Runways"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapShowRunways = false;
			}
			else
			{
				if (GUILayout.Button(new GUIContent(tRunwaysOff, "Runways"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapShowRunways = true;
			}

			if (KerbalKonstructs.instance.mapShowOther)
			{
				if (GUILayout.Button(new GUIContent(tOtherOn, "Other"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapShowOther = false;
			}
			else
			{
				if (GUILayout.Button(new GUIContent(tOtherOff, "Other"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapShowOther = true;
			}

			GUILayout.Box(" ", BoxNoBorder, GUILayout.Height(34));

			GUI.enabled = (MiscUtils.isCareerGame());
			if (!MiscUtils.isCareerGame())
			{
				GUILayout.Button(tUplinksOff, GUILayout.Width(32), GUILayout.Height(32));
				GUILayout.Button(tRadarOff, GUILayout.Width(32), GUILayout.Height(32));
				GUILayout.Button(tGroundCommsOff, GUILayout.Width(32), GUILayout.Height(32));
			}
			else
			{
				if (KerbalKonstructs.instance.mapShowUplinks)
				{
					if (GUILayout.Button(new GUIContent(tUplinksOn, "Uplinks"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowUplinks = false;
				}
				else
				{
					if (GUILayout.Button(new GUIContent(tUplinksOff, "Uplinks"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowUplinks = true;
				}

				if (KerbalKonstructs.instance.mapShowRadar)
				{
					if (GUILayout.Button(new GUIContent(tRadarOn, "Radar"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowRadar = false;
				}
				else
				{
					if (GUILayout.Button(new GUIContent(tRadarOff, "Radar"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowRadar = true;
				}

				if (KerbalKonstructs.instance.mapShowGroundComms)
				{
					if (GUILayout.Button(new GUIContent(tGroundCommsOn, "Ground Comms"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowGroundComms = false;
				}
				else
				{
					if (GUILayout.Button(new GUIContent(tGroundCommsOff, "Ground Comms"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
						KerbalKonstructs.instance.mapShowGroundComms = true;
				}

			}
			GUI.enabled = true;

			GUILayout.Box(" ", BoxNoBorder, GUILayout.Height(34));

			if (KerbalKonstructs.instance.mapHideIconsBehindBody)
			{
				if (GUILayout.Button(new GUIContent(tHideOn, "Occlude"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapHideIconsBehindBody = false;
			}
			else
			{
				if (GUILayout.Button(new GUIContent(tHideOff, "Occlude"), ButtonKK, GUILayout.Width(32), GUILayout.Height(32)))
					KerbalKonstructs.instance.mapHideIconsBehindBody = true;
			}

			GUILayout.Box(" ", BoxNoBorder, GUILayout.Height(34));

			if (GUILayout.Button("X", ButtonRed, GUILayout.Height(20), GUILayout.Width(20)))
			{
				loadedPersistence = false;
				KerbalKonstructs.instance.showMapIconManager = false;
			}

			GUILayout.EndHorizontal();

			/* float fFOV = MapView.MapCamera.GetComponent<Camera>().fieldOfView;
			float fFCP = MapView.MapCamera.GetComponent<Camera>().farClipPlane;
			float fNCP = MapView.MapCamera.GetComponent<Camera>().nearClipPlane;
			Vector3 CamPos = MapView.MapCamera.GetComponent<Camera>().ScreenToWorldPoint(ScaledSpace.ScaledToLocalSpace(PlanetariumCamera.fetch.target.gameObject.transform.position));
			float fDistCam = Vector3.Distance(CamPos, PlanetariumCamera.fetch.target.gameObject.transform.position);

			GUILayout.Label(" Pos: x" + CamPos.x.ToString("#0.000") + "y" + CamPos.y.ToString("#0.000") + "z" + CamPos.z.ToString("#0.000") + " Dist " + fDistCam.ToString("0.0"));
			*/


			if (GUI.tooltip != "")
			{
				var labelSize = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(GUI.tooltip));
				GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y + 20, labelSize.x + 5, labelSize.y + 6), GUI.tooltip, KKToolTip);
			}
			
			GUI.DragWindow(new Rect(0, 0, 10000, 10000));
		}

		static Material lineMaterial1;
		static Material lineMaterial2;
		static Material lineMaterial3;
		static Material lineMaterial4;

		static void CreateLineMaterial(int iMat = 1)
		{
			Material mMat = lineMaterial1;
			if (iMat == 2) mMat = lineMaterial2;
			if (iMat == 3) mMat = lineMaterial3;
			if (iMat == 4) mMat = lineMaterial4;

			if (mMat == null)
			{
				// Unity has a built-in shader that is useful for drawing
				// simple colored things.
				var shader = Shader.Find("Hidden/Internal-Colored");
				mMat = new Material(shader);
				mMat.hideFlags = HideFlags.HideAndDontSave;
				// Turn on alpha blending
				mMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				mMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				// Turn backface culling off
				mMat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
				// Turn off depth writes
				mMat.SetInt("_ZWrite", 0);

				if (iMat == 1) lineMaterial1 = mMat;
				if (iMat == 2) lineMaterial2 = mMat;
				if (iMat == 3) lineMaterial3 = mMat;
				if (iMat == 4) lineMaterial4 = mMat;
			}
		}

		public void drawTrackingStations()
		{
			displayingTooltip2 = false;
			MapObject target = PlanetariumCamera.fetch.target;
			string Base = "";
			string Base2 = "";
			float Range = 0f;
			LaunchSite lBase = null;
			LaunchSite lBase2 = null;

			// Do tracking stations first
			foreach (StaticObject obj in KerbalKonstructs.instance.getStaticDB().getAllStatics())
			{
				if (!MiscUtils.isCareerGame()) break;

				bool display2 = false;
				string openclosed3 = "Closed";

				if ((string)obj.getSetting("FacilityType") != "TrackingStation")
					continue;

				if (isOccluded(obj.gameObject.transform.position, target.celestialBody))
				{
					if (KerbalKonstructs.instance.mapHideIconsBehindBody)
						continue;
				}

				openclosed3 = (string)obj.getSetting("OpenCloseState");

				if ((float)obj.getSetting("OpenCost") == 0) openclosed3 = "Open";

				if (KerbalKonstructs.instance.mapShowOpenT)
					display2 = true;
				if (!KerbalKonstructs.instance.mapShowClosed && openclosed3 == "Closed")
					display2 = false;
				if (!KerbalKonstructs.instance.mapShowOpen && openclosed3 == "Open")
					display2 = false;

				if (!display2)
					continue;

				Vector3 pos = MapView.MapCamera.GetComponent<Camera>().WorldToScreenPoint(ScaledSpace.LocalToScaledSpace(obj.gameObject.transform.position));
				Rect screenRect6 = new Rect((pos.x - 8), (Screen.height - pos.y) - 8, 16, 16);
				Graphics.DrawTexture(screenRect6, TrackingStationIcon);

				string sTarget = (string)obj.getSetting("TargetID");
				float fStRange = (float)obj.getSetting("TrackingShort");
				float fStAngle = (float)obj.getSetting("TrackingAngle");

				if (openclosed3 == "Open" && KerbalKonstructs.instance.mapShowGroundComms)
				{
					LaunchSiteManager.getNearestBase(obj.gameObject.transform.position, out Base, out Base2, out Range, out lBase, out lBase2);
					Vector3 vNeighbourPos = Vector3.zero;
					Vector3 vNeighbourPos2 = Vector3.zero;
					Vector3 vBasePos = Vector3.zero;
					Vector3 vBasePos2 = Vector3.zero;

					GameObject goNeighbour = null;
					GameObject goNeighbour2 = null;

					if (Base != "")
					{
						if (Base == "KSC")
						{
							goNeighbour = SpaceCenterManager.KSC.gameObject;
						}
						else
							goNeighbour = LaunchSiteManager.getSiteGameObject(Base);
					}

					if (Base2 != "")
					{ 

						if (Base2 == "KSC")
						{
							goNeighbour2 = SpaceCenterManager.KSC.gameObject;
						}
						else
							goNeighbour2 = LaunchSiteManager.getSiteGameObject(Base2);
					}

					if (goNeighbour != null)
					{
						vNeighbourPos = goNeighbour.transform.position;
						vBasePos = MapView.MapCamera.GetComponent<Camera>().WorldToScreenPoint(ScaledSpace.LocalToScaledSpace(vNeighbourPos));
					}

					if (goNeighbour2 != null)
					{
						vNeighbourPos2 = goNeighbour2.transform.position;
						vBasePos2 = MapView.MapCamera.GetComponent<Camera>().WorldToScreenPoint(ScaledSpace.LocalToScaledSpace(vNeighbourPos2));
					}

					if (goNeighbour != null && vNeighbourPos != Vector3.zero && vBasePos != Vector3.zero)
					{
						CreateLineMaterial(1);

						GL.Begin(GL.LINES);
						lineMaterial1.SetPass(0);
						GL.Color(new Color(1f, 1f, 1f, 0.7f));
						GL.Vertex3(pos.x - Screen.width / 2, pos.y - Screen.height / 2, pos.z);
						GL.Vertex3(vBasePos.x - Screen.width / 2, vBasePos.y - Screen.height / 2, vBasePos.z);
						GL.End();
					}

					if (goNeighbour2 != null && vNeighbourPos2 != Vector3.zero && vBasePos2 != Vector3.zero)
					{
						CreateLineMaterial(2);

						GL.Begin(GL.LINES);
						lineMaterial2.SetPass(0);
						GL.Color(new Color(1f, 1f, 1f, 0.7f));
						GL.Vertex3(pos.x - Screen.width / 2, pos.y - Screen.height / 2, pos.z);
						GL.Vertex3(vBasePos2.x - Screen.width / 2, vBasePos2.y - Screen.height / 2, vBasePos2.z);
						GL.End();
					}
				}

				if ((string)obj.getSetting("TargetType") == "Craft" && sTarget != "None")
				{
					Vessel vTargetVessel = TrackingStationGUI.GetTargetVessel(sTarget);
					if (vTargetVessel == null)
					{ }
					else
					{
						if (vTargetVessel.state == Vessel.State.DEAD)
						{ }
						else
						{
							CelestialBody cbTStation = (CelestialBody)obj.getSetting("CelestialBody");
							CelestialBody cbTCraft = vTargetVessel.mainBody;

							if (cbTStation == cbTCraft && openclosed3 == "Open" && KerbalKonstructs.instance.mapShowUplinks)
							{
								Vector3 vCraftPos = MapView.MapCamera.GetComponent<Camera>().WorldToScreenPoint(ScaledSpace.LocalToScaledSpace(vTargetVessel.gameObject.transform.position));

								float fRangeToTarget = TrackingStationGUI.GetRangeToCraft(obj, vTargetVessel);
								int iUplink = TrackingStationGUI.GetUplinkQuality(fStRange, fRangeToTarget);
								float fUplink = (float)iUplink / 100;

								float fRed = 1f;
								float fGreen = 0f;
								float fBlue = fUplink;
								float fAlpha = 1f;

								if (iUplink > 45)
								{
									fRed = 1f;
									fGreen = 0.65f + (fUplink / 10);
									fBlue = 0f;
								}

								if (iUplink > 85)
								{
									fRed = 0f;
									fGreen = fUplink;
									fBlue = 0f;
								}

								float fStationLOS = TrackingStationGUI.StationHasLOS(obj, vTargetVessel);

								if (fStationLOS > fStAngle)
								{
									fRed = 1f;
									fGreen = 0f;
									fBlue = 0f;
									fAlpha = 0.5f;
								}

								CreateLineMaterial(3);

								GL.Begin(GL.LINES);
								lineMaterial3.SetPass(0);
								GL.Color(new Color(fRed, fGreen, fBlue, fAlpha));
								GL.Vertex3(pos.x - Screen.width / 2, pos.y - Screen.height / 2, pos.z);
								GL.Vertex3(vCraftPos.x - Screen.width / 2, vCraftPos.y - Screen.height / 2, vCraftPos.z);
								GL.End();
							}
						}
					}
				}

				if (screenRect6.Contains(Event.current.mousePosition) && !displayingTooltip2)
				{
					CelestialBody cPlanetoid = (CelestialBody)obj.getSetting("CelestialBody");

					var objectpos2 = cPlanetoid.transform.InverseTransformPoint(obj.gameObject.transform.position);
					var dObjectLat2 = NavUtils.GetLatitude(objectpos2);
					var dObjectLon2 = NavUtils.GetLongitude(objectpos2);
					var disObjectLat2 = dObjectLat2 * 180 / Math.PI;
					var disObjectLon2 = dObjectLon2 * 180 / Math.PI;

					if (disObjectLon2 < 0) disObjectLon2 = disObjectLon2 + 360;

					//Only display one tooltip at a time
					displayMapIconToolTip("Tracking Station " + "\n(Lat." + disObjectLat2.ToString("#0.00") + "/ Lon." + disObjectLon2.ToString("#0.00") + ")", pos);

					if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
					{
						float sTrackAngle = (float)obj.getSetting("TrackingAngle");
						float sTrackRange = (float)obj.getSetting("TrackingShort");

						PersistenceUtils.loadStaticPersistence(obj);

						float sTrackAngle2 = (float)obj.getSetting("TrackingAngle");
						float sTrackRange2 = (float)obj.getSetting("TrackingShort");

						selectedFacility = obj;
						FacilityManager.setSelectedFacility(obj);
						KerbalKonstructs.instance.showFacilityManager = true;
					}
				}
			}
		}
		
		public void drawIcons()
		{
			displayingTooltip = false;
			MapObject target = PlanetariumCamera.fetch.target;

			if (target.type != MapObject.ObjectType.CelestialBody) return;

			drawTrackingStations();
			
			// Then do launchsites
			List<LaunchSite> sites = LaunchSiteManager.getLaunchSites();
			foreach (LaunchSite site in sites)
			{
				PSystemSetup.SpaceCenterFacility facility = PSystemSetup.Instance.GetSpaceCenterFacility(site.name);

				if (facility == null) 
					continue;

				PSystemSetup.SpaceCenterFacility.SpawnPoint sp = facility.GetSpawnPoint(site.name);

				if (sp == null) 
					continue;

				if (facility.facilityPQS != target.celestialBody.pqsController)
					continue;

				if (isOccluded(sp.GetSpawnPointTransform().position, target.celestialBody))
				{
					if (KerbalKonstructs.instance.mapHideIconsBehindBody) continue;
				}

				Vector3 pos = MapView.MapCamera.GetComponent<Camera>().WorldToScreenPoint(ScaledSpace.LocalToScaledSpace(sp.GetSpawnPointTransform().position));
				Rect screenRect = new Rect((pos.x - 8), (Screen.height - pos.y) - 8, 16, 16);

				// Distance between camera and spawnpoint sort of
				float fPosZ = pos.z;

				float fRadarRadius = 12800 / fPosZ;
				float fRadarOffset = fRadarRadius / 2;
									
				Rect screenRect2 = new Rect((pos.x -fRadarOffset), (Screen.height - pos.y) -fRadarOffset, fRadarRadius, fRadarRadius);
				Rect screenRect3 = new Rect((pos.x - (fRadarOffset / 2)), (Screen.height - pos.y) - (fRadarOffset / 2), fRadarRadius / 2, fRadarRadius / 2);
				Rect screenRect4 = new Rect((pos.x - (fRadarOffset / 3)), (Screen.height - pos.y) - (fRadarOffset / 3), fRadarRadius / 3, fRadarRadius / 3);
				Rect screenRect5 = new Rect((pos.x - (fRadarOffset / 4)), (Screen.height - pos.y) - (fRadarOffset / 4), fRadarRadius / 4, fRadarRadius / 4);

				string openclosed = site.openclosestate;
				string category = site.category;

				bool display = true;

				if (!KerbalKonstructs.instance.mapShowHelipads && category == "Helipad")
					display = false;
				if (!KerbalKonstructs.instance.mapShowOther && category == "Other")
					display = false;
				if (!KerbalKonstructs.instance.mapShowRocketbases && category == "RocketPad")
					display = false;
				if (!KerbalKonstructs.instance.mapShowRunways && category == "Runway")
					display = false;

				if (display && MiscUtils.isCareerGame())
				{
					if (!KerbalKonstructs.instance.mapShowOpen && openclosed == "Open")
						display = false;
					if (!KerbalKonstructs.instance.mapShowClosed && openclosed == "Closed")
						display = false;
					if (KerbalKonstructs.instance.disableDisplayClosed && openclosed == "Closed")
						display = false;
					if (openclosed == "OpenLocked" || openclosed == "ClosedLocked")
						display = false;
				}

				if (!display) continue;

				if (fRadarRadius > 15)
				{
					if (category == "Runway" && KerbalKonstructs.instance.mapShowRadar)
					{
						Graphics.DrawTexture(screenRect2, tRadarCover);
						Graphics.DrawTexture(screenRect3, tRadarCover);
						Graphics.DrawTexture(screenRect4, tRadarCover);
						Graphics.DrawTexture(screenRect5, tRadarCover);
					}

					if (category == "Helipad" && KerbalKonstructs.instance.mapShowRadar)
					{
						Graphics.DrawTexture(screenRect3, tRadarCover);
						Graphics.DrawTexture(screenRect4, tRadarCover);
						Graphics.DrawTexture(screenRect5, tRadarCover);
					}
				}
										
				if (site.icon != null)
				{
					Graphics.DrawTexture(screenRect, site.icon);
				}
				else
				{
					switch (site.type)
					{
						case SiteType.VAB:
							Graphics.DrawTexture(screenRect, VABIcon);
							break;
						case SiteType.SPH:
							Graphics.DrawTexture(screenRect, SPHIcon);
							break;
						default:
							Graphics.DrawTexture(screenRect, ANYIcon);
							break;
					}
				}

				// Tooltip
				if (screenRect.Contains(Event.current.mousePosition) && !displayingTooltip)
				{
					//Only display one tooltip at a time
					string sToolTip = "";
					sToolTip = site.name;
					if (site.name == "Runway") sToolTip = "KSC Runway";
					if (site.name == "LaunchPad") sToolTip = "KSC LaunchPad";
					displayMapIconToolTip(sToolTip, pos);

					// Select a base by clicking on the icon
					if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
					{
						MiscUtils.HUDMessage("Selected base is " + sToolTip + ".", 5f, 3);
						BaseManager.setSelectedSite(site);
						selectedSite = site;
						NavGuidanceSystem.setTargetSite(selectedSite);
						KerbalKonstructs.instance.showBaseManager = true;
					}
				}
			}
		}

		private bool isOccluded(Vector3d loc, CelestialBody body)
		{
			Vector3d camPos = ScaledSpace.ScaledToLocalSpace(PlanetariumCamera.Camera.transform.position);

			if (Vector3d.Angle(camPos - loc, body.position - loc) > 90)
				return false;

			return true;
		}
	}
}