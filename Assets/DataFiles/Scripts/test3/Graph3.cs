using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Graph3 : MonoBehaviour {

	public Transform pointPrefab;
	const float pi = Mathf.PI;
	public GameObject canvas;
	public GameObject scroll;

	bool trigger_canvas = false;
	int resolution = 30;

	private void DestroyChildren(){
		if (transform.childCount > 0) {
			for (int i = 0; i < transform.childCount; i++) {
				Destroy (transform.GetChild (i).gameObject);
			}
		}
	}

	void Start(){
		canvas.SetActive (false);
		scroll.SetActive (false);
	}

	public void ShowCanvas(){
		trigger_canvas = !trigger_canvas;
		canvas.SetActive (trigger_canvas);
		scroll.SetActive (trigger_canvas);
	}
		

	private void Rainbow(){
		float red = 1f;
		float green = 0f;
		float blue = 0f;
		int index = 0;
		int portions = transform.childCount/6;
		float stepColor = 1f / (float)portions;
		int ratio = transform.childCount/6;
		/*
		Debug.Log ("PORTIONS  ---> "+portions);
		Debug.Log ("STEP COLOR  ---> "+stepColor);
		Debug.Log ("RATIO ---->"+ratio);

		Debug.Log("CHILD COUNT ---->"+ transform.childCount);

		//Debug.Log (red + " " + green + " " +blue);
		*/
		//red to yellow
		for (int i = 0; i < portions; i ++) {
			//Debug.Log ("RED TO YELLOW -> "+ red +" -- "+ green +" -- "+ blue);
			if (i == portions-1) {
				index = i;
				portions = ratio*2;
				break;
			}
			transform.GetChild (i).GetComponent<Renderer> ().material.color = new Color (red, green, blue);
			green += stepColor;
		}

		//yellow to lime
		for (int i = index; i < portions; i ++) {
			//Debug.Log ("YELLOW TO LIME -> "+ red +" -- "+ green +" -- "+ blue);
			if (i == portions-1) {
				index = i;
				portions = ratio*3;
				break;
			}
			transform.GetChild (i).GetComponent<Renderer> ().material.color = new Color (red, green, blue);
			red -= stepColor;
		}

		//lime to cyan
		for (int i = index; i < portions; i ++) {
			//Debug.Log ("LIME TO CYAN -> "+ red +" -- "+ green +" -- "+ blue);
			if (i == portions-1) {
				index = i;
				portions = ratio*4;
				break;
			}
			transform.GetChild (i).GetComponent<Renderer> ().material.color = new Color (red, green, blue);
			blue += stepColor;
		}

		//cyan to blue
		for (int i = index; i < portions; i ++) {
			//Debug.Log ("CYAN TO BLUE -> "+ red +" -- "+ green +" -- "+ blue);
			if (i == portions-1) {
				index = i;
				portions = ratio*5;
				break;
			}
			transform.GetChild (i).GetComponent<Renderer> ().material.color = new Color (red, green, blue);
			green -= stepColor;
		}

		//blue to magenta
		for (int i = index; i < portions; i ++) {
			//Debug.Log ("BLUE TO MAGENTA -> "+ red +" -- "+ green +" -- "+ blue);
			if (i == portions-1) {
				index = i;
				portions = ratio*6;
				break;
			}
			transform.GetChild (i).GetComponent<Renderer> ().material.color = new Color (red, green, blue);
			red += stepColor;
		}

		//magenta to red 
		for (int i = index; i < portions; i ++) {
			//Debug.Log ("MAGENTA TO RED -> "+ red +" -- "+ green +" -- "+ blue);
			if (i == portions-1) {
				index = i;
				portions = ratio*7;
				break;
			}
			transform.GetChild (i).GetComponent<Renderer> ().material.color = new Color (red, green, blue);
			blue -= stepColor;
		}

		for (int i = index; i < transform.childCount; i ++) {
			transform.GetChild (i).GetComponent<Renderer> ().material.color = new Color (red, green, blue);
			red += stepColor;
		}
	}

	private void Torus(){

		float R = 1f;
		float r = 0.5f;

		float step = 2f / resolution;
		float stepT = pi * 2 / resolution;
		float stepP = pi * 2f / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float t = 0f; t < 2f * pi; t += stepT) {
			for (float p = 0; p < 2f * pi; p += stepP) {
				Transform point = Instantiate (pointPrefab);
				position.x = (R + r * Mathf.Cos (p)) * Mathf.Cos (t);
				position.y = (R + r * Mathf.Cos (p)) * Mathf.Sin (t);
				position.z = r * Mathf.Sin (p);
				point.localPosition = position * 60f;
				point.localScale = new Vector3(7f,7f,7f);
				point.SetParent(transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void Moebius(){
		DestroyChildren ();

		float step = 2f / resolution;
		float stepV = 1f / resolution;
		float stepU = pi * 2f / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = -1f; v < 1f; v+=stepV) {
			for (float u = 0; u < 2f*pi; u+=stepU) {
				Transform point = Instantiate(pointPrefab);
				position.x = (1+(v/2f)*Mathf.Cos(u/2f))*Mathf.Cos(u);
				position.y = (1+(v/2f)*Mathf.Cos(u/2f))*Mathf.Sin(u);
				position.z = (v/2f)*Mathf.Sin(u/2f);
				point.localPosition = position * 60f;
				point.localScale = new Vector3(7f,7f,7f);
				point.SetParent(transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);	
	}

	private void Paraboloid(){

		DestroyChildren ();
		float a = 2f;
		float h = 2f;

		float step = 2f / resolution;
		float stepV = pi*2 / resolution;
		float stepU = pi * 2f / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = 0f; v < 2f*pi; v+=stepV) {
			for (float u = 0; u < 2f*pi; u+=stepU) {
				Transform point = Instantiate(pointPrefab);
				position.x = a*Mathf.Sqrt(u/h)*Mathf.Cos(v);
				position.y = a*Mathf.Sqrt(u/h)*Mathf.Sin(v);
				position.z = u;
				point.localPosition = position * 20f;
				point.localScale = new Vector3(6f,6f,6f);
				point.SetParent(transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (-90, 0, 0);
	}

	private void Ripple(){
		DestroyChildren ();
		float step = 2f / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (int i = 0, z = 0; z < resolution; z++) {
			float v = (z + 0.5f) * step - 1f;
			for (int x = 0; x < resolution; x++, i++) {
				float u = (x + 0.5f) * step - 1f;
				Transform point = Instantiate(pointPrefab);
				float d = Mathf.Sqrt(u * u + v * v);
				position.x = u;
				position.y = Mathf.Sin(pi * (4f * d));
				position.y /= 1f + 10f * d;
				position.z = v;
				point.localPosition = position * 80f;
				point.localScale = new Vector3(6f,6f,6f);
				point.SetParent(transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void Klein(){
		DestroyChildren ();
		float step = 2f / resolution;
		float stepV = pi*2 / resolution;
		float stepU = pi * 2f / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = 0f; v < 2f*pi; v+=stepV) {
			for (float u = 0; u < 2f*pi; u+=stepU) {
				float r = 4f * (1f - (Mathf.Cos (u) / 2f));
				Transform point = Instantiate(pointPrefab);
				if (u > 0f && u < pi) {
					position.x = 6f * Mathf.Cos (u) * (1f + Mathf.Sin (u)) + r * Mathf.Cos (u) * Mathf.Cos (v);
					position.y = 16f * Mathf.Sin (u) + r * Mathf.Sin (u) * Mathf.Cos (v);
					position.z = r*Mathf.Sin(v);
					point.localPosition = position * 6f;

				} else if (u > pi && u < 2f * pi) {
					position.x = 6f*Mathf.Cos(u)*(1f+Mathf.Sin(u))+r*Mathf.Cos(v+pi);
					position.y = 16f * Mathf.Sin (u);
					position.z = r*Mathf.Sin(v);
					point.localPosition = position * 6f;

				}

				point.localScale = new Vector3(7f,7f,7f);
				point.SetParent(transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}


	private void Boys(){
		DestroyChildren ();
		float step = 2f / resolution;
		float stepV = pi / resolution;
		float stepU = pi / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = 0f; v < pi; v += stepV) {
			for (float u = 0; u < pi; u += stepU) {
				Transform point = Instantiate (pointPrefab);
				position.x = (2f / 3f) * (Mathf.Cos (u) * Mathf.Cos (2f * v) + Mathf.Sqrt (2f) * Mathf.Sin (u) * Mathf.Cos (v)) * Mathf.Cos (u) / (Mathf.Sqrt (2f) - Mathf.Sin (2f * u) * Mathf.Sin (3f * v));
				position.y = (2f / 3f) * (Mathf.Cos (u) * Mathf.Sin (2f * v) - Mathf.Sqrt (2f) * Mathf.Sin (u) * Mathf.Sin (v)) * Mathf.Cos (u) / (Mathf.Sqrt (2f) - Mathf.Sin (2f * u) * Mathf.Sin (3f * v));
				position.z = Mathf.Sqrt (2f) * Mathf.Pow (Mathf.Cos (u), 2f) / (Mathf.Sqrt (2f) - Mathf.Sin (2f * u) * Mathf.Sin (2f * v));
				point.localPosition = position * 90f;
				point.localScale = new Vector3(7f,7f,7f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void Pseudosphere(){
		DestroyChildren ();
		float step = 2f / resolution;
		float stepV = pi / resolution;
		float stepU = 2f*pi / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = 0f; v < pi; v += stepV) {
			for (float u = 0; u < 2f*pi; u += stepU) {
				Transform point = Instantiate (pointPrefab);
				position.x = Mathf.Cos (u) * Mathf.Sin (v);
				position.y = Mathf.Sin (u) * Mathf.Sin (v);
				position.z = Mathf.Cos (v) + Mathf.Log (Mathf.Tan((1f/2f)*v));
				point.localPosition = position * 60f;
				point.localScale = new Vector3(4f,4f,4f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void Sphere(){
		DestroyChildren ();
		float step = 2f / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (int i = 0, z = 0; z < resolution; z++) {
			float v = (z + 0.5f) * step - 1f;
			for (int x = 0; x < resolution; x++, i++) {
				float u = (x + 0.5f) * step - 1f;
				Transform point = Instantiate (pointPrefab);
				float r = Mathf.Cos(pi * 0.5f * v);
				position.x = r* Mathf.Sin(pi * u);
				position.y = Mathf.Sin(pi * 0.5f * v);
				position.z = r* Mathf.Cos(pi * u);
				point.localPosition = position * 60f;
				point.localScale = new Vector3(4f,4f,4f);
				point.SetParent (transform,false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void Cone(){
		DestroyChildren ();
		float step = 2f / resolution;
		float stepV = 2f*pi / resolution;
		float stepU = 2f*pi / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = 0f; v < 2f*pi; v += stepV) {
			for (float u = 0; u < 2f*pi; u += stepU) {
				Transform point = Instantiate (pointPrefab);
				position.x = u * Mathf.Cos (v);
				position.y = u * Mathf.Sin (v);
				position.z = u;
				point.localPosition = position * 10f;
				point.localScale = new Vector3(7f,7f,7f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (-90, 0, 0);
	}

	private void Catenoid(){
		DestroyChildren ();
		float step = 2f / resolution;
		float stepZ = 2f / resolution;
		float stepTheta = 2f*pi / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float theta = 0f; theta < 2f*pi; theta += stepTheta) {
			for (float z = -2f; z < 2f; z += stepZ) {
				Transform point = Instantiate (pointPrefab);
				position.x = Mathf.Sqrt(4+Mathf.Pow(z,2))*Mathf.Cos(theta);
				position.y = Mathf.Sqrt(4+Mathf.Pow(z,2))*Mathf.Sin(theta);
				position.z = z;
				point.localPosition = position * 20f;
				point.localScale = new Vector3(7f,7f,7f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (-90, 0, 0);
	}

	private void Dini(){
		DestroyChildren ();
		float a = 1f;
		float b = 0.2f;

		float step = 2f / resolution;
		float stepU = 4f*pi / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = 0f; v < 2; v += step) {
			for (float u = 0; u < 4f*pi; u += stepU) {
				Transform point = Instantiate (pointPrefab);
				position.x = a * Mathf.Cos (u) * Mathf.Sin (v);
				position.y = a * Mathf.Sin (u) * Mathf.Sin (v);
				position.z = a*(Mathf.Cos(v)+Mathf.Log(Mathf.Tan((1f/2f)*v)))+b*u;
				point.localPosition = position * 40f;
				point.localScale = new Vector3(7f,7f,7f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (-90, 0, 0);
	}

	private void Breathe(){
		DestroyChildren ();
		float a = 2f/5f;
		float step = 2f / resolution;
		float stepV = 37.4f / resolution;
		float stepU = 14f / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = -37.4f; v < 37.4f; v += stepV) {
			for (float u = -14f; u < 14f; u += stepU) {
				Transform point = Instantiate (pointPrefab);
				float cosh = (Mathf.Exp(a*u)+Mathf.Exp(-a*u))/2f;
				float sinh= (Mathf.Exp(a*u)-Mathf.Exp(-a*u))/2f;
				position.x = -u+((2f*(1f-Mathf.Pow(a,2f))*cosh*sinh)/(a*((1f-Mathf.Pow(a,2f))*cosh*cosh+Mathf.Pow(a,2f)*Mathf.Pow(Mathf.Sin(Mathf.Sqrt(1f-Mathf.Pow(a,2f))*v),2f))));
				position.y = (2f*Mathf.Sqrt(1f-a-Mathf.Pow(a,2))*(-Mathf.Sqrt(1f-Mathf.Pow(a,2))*Mathf.Cos(v)*Mathf.Cos(Mathf.Sqrt(1f-Mathf.Pow(a,2))*v)-Mathf.Sin(v)*Mathf.Sin(Mathf.Sqrt(1f-Mathf.Pow(a,2)))))/(a*((a-Mathf.Pow(a,2))*cosh*cosh+Mathf.Pow(a,2)*Mathf.Pow(Mathf.Sin(Mathf.Sqrt(1f-Mathf.Pow(a,2))),2)));
				position.z = (2f*Mathf.Sqrt(1f-a-Mathf.Pow(a,2))*(-Mathf.Sqrt(1f-Mathf.Pow(a,2))*Mathf.Sin(v)*Mathf.Cos(Mathf.Sqrt(1f-Mathf.Pow(a,2))*v)-Mathf.Cos(v)*Mathf.Sin(Mathf.Sqrt(1f-Mathf.Pow(a,2)))))/(a*((a-Mathf.Pow(a,2))*cosh*cosh+Mathf.Pow(a,2)*Mathf.Pow(Mathf.Sin(Mathf.Sqrt(1f-Mathf.Pow(a,2))),2)));
				point.localPosition = position * 5f;
				point.localScale = new Vector3(4f,4f,4f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void Helicoid(){
		DestroyChildren ();
		float alpha = 1f;
		float step = 2f / resolution;
		float stepRho = 1f / resolution;
		float stepTheta = pi / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float rho = -1f; rho < 1f; rho += stepRho) {
			for (float theta = 0; theta < pi; theta += stepTheta) {
				Transform point = Instantiate (pointPrefab);
				position.x = rho*Mathf.Cos(alpha*theta);
				position.y = rho*Mathf.Sin(alpha*theta);
				position.z = theta;
				point.localPosition = position * 40f;
				point.localScale = new Vector3(4f,4f,4f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (-90, 0, 0);
	}

	private void Snailshell(){
		DestroyChildren ();
		float step = 2f / resolution;
		float stepV = 2f*pi / resolution;
		float stepU = 6f*pi / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float v = 0; v < 2f*pi; v += stepV) {
			for (float u = 0; u < 6f*pi; u += stepU) {
				Transform point = Instantiate (pointPrefab);
				position.x = 2f*(1f-Mathf.Exp(u/(6f*pi)))*Mathf.Cos(u)*Mathf.Pow(Mathf.Cos((1f/2f)/v),2);
				position.y = 2f * (-1f + Mathf.Exp (u / (6f * pi))) * Mathf.Sin (u) * Mathf.Pow (Mathf.Cos ((1f / 2f) / v), 2);
				position.z = 1f - Mathf.Exp(u/(3f*pi))-Mathf.Sin(v)+Mathf.Exp(u/(6f*pi))*Mathf.Sin(v);
				point.localPosition = position * 20f;
				point.localScale = new Vector3(5f,5f,5f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (-90, 0, 0);
	}

	private void Hellipsoid(){
		DestroyChildren ();
		float a = 4.5f;
		float b = 6f;
		float c = 3f;

		float step = 2f / resolution;
		float stepPhi = pi / resolution;
		float stepTheta = (pi/2f) / resolution;
		Vector3 position;
		Vector3 scale = Vector3.one * step;
		for (float phi = -pi; phi < pi; phi += stepPhi) {
			for (float theta = -(pi/2f); theta < pi/2f; theta += stepTheta) {
				Transform point = Instantiate (pointPrefab);
				position.x = a * Mathf.Cos (theta) * Mathf.Cos (phi);
				position.y = b * Mathf.Cos (theta) * Mathf.Sin (phi);
				position.z = c * Mathf.Sin (theta);
				point.localPosition = position * 20f;
				point.localScale = new Vector3(4f,4f,4f);
				point.SetParent (transform, false);
			}
		}
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	public void changeSurface(){
		DestroyChildren ();
		float time = Time.time;

		StartCoroutine (Wait());

	}

	IEnumerator Wait(){
		yield return new WaitUntil(() => transform.childCount==0);
		SurfaceCreator ();
	}

	private void SurfaceCreator(){
		int selectedSurface = int.Parse(EventSystem.current.currentSelectedGameObject.name);

		switch (selectedSurface) {
		case 0:
			Moebius ();
			break;
		case 1:
			Torus();
			break;
		case 2:
			Paraboloid ();
			break; 
		case 3:
			Ripple ();
			break;
		case 4:
			Klein ();
			break;
		case 5:
			Boys ();
			break;
		case 6: 
			Pseudosphere ();
			break;
		case 7: 
			Sphere ();
			break;
		case 8:
			Cone ();
			break;
		case 9:
			Catenoid ();
			break;
		case 10:
			Dini ();
			break;
		case 11:
			Breathe ();
			break;
		case 12:
			Helicoid ();
			break;
		case 13:
			Snailshell ();
			break;
		case 14:
			Hellipsoid ();
			break;
		default:
			return;
		}
		Rainbow ();
	}
}
