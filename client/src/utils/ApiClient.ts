import axios from "axios";

module ApiClient {
  export async function PostAsync<T, R>(path: string, req: R, headers?: object): Promise<R> {
    try {
      const { data } = await axios.post(
        `${process.env.REACT_APP_API_URI}/api/${path}`,
        req,
        headers
      );
      return data as R;
    } catch (err: any) {
      if (err.response.status === 401) {
      }
      return err.response.data as R;
    }
  }
}

export default ApiClient;
