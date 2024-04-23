
module ApiClient {

  export async function GetAsync<R>(path: string, req?: RequestInit): Promise<R | undefined>{
    try {
      const response = await fetch(`${process.env.REACT_APP_API_URI}/api/${path}`, req).then(data=>data.json());
      return response as R;
    } catch (error: any) {
      return error;
    }
  } 

  export async function PostAsync<R>(path: string, req?: RequestInit): Promise<R | undefined> {
    try {
      const response = await fetch(`${process.env.REACT_APP_API_URI}/api/${path}`,req).then(data=>data.json());
      return response as R;
    } catch (error: any) {
      return error;
    }
  }
}

export default ApiClient;
