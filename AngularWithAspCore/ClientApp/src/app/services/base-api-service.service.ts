import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiRes } from '../models/api-res.model';
import { AppConsts } from '../misc/app.consts';
import { AuthService } from '../services/auth-service.service';


@Injectable({
  providedIn: 'root'
})
export class BaseApiService {

  private skipAuthHeaders = new HttpHeaders().set(AppConsts.interceptorSkipAuthHeader, '');
  private authService = new AuthService();

    constructor(private httpClient: HttpClient) {
    }

    private mapHttpParams(params: any): HttpParams {
        let httpParams = new HttpParams();
        for (let x in params) {
            let val = params[x];
            if (typeof val === 'object') {
                val = JSON.stringify(val);
            }
            else {
                val = val.toString();
            }
            httpParams = httpParams.append(x, val);
        }

        return httpParams;
    }

    public get(url: string): Observable<ApiRes> {
        return this.httpClient.get<ApiRes>(url);
    }

    public getWithoutAuth(url: string): Observable<ApiRes> {
        return this.httpClient.get<ApiRes>(url, { headers: this.skipAuthHeaders });
    }

    public getFile(url: string): Observable<ArrayBuffer> {
        return this.httpClient.get(url, { responseType: 'arraybuffer' });
    }

    public getFileWithoutAuth(url: string): Observable<ArrayBuffer> {
        return this.httpClient.get(url, { responseType: 'arraybuffer', headers: this.skipAuthHeaders });
    }

    public getByParams(url: string, params: any): Observable<ApiRes> {
        return this.httpClient.get<ApiRes>(url, { params: this.mapHttpParams(params) });
    }

    public getByParamsWithoutAuth(url: string, params: any): Observable<ApiRes> {
        return this.httpClient.get<ApiRes>(url, { params: this.mapHttpParams(params), headers: this.skipAuthHeaders });
    }

    public post<T>(url: string, data: T): Observable<ApiRes> {
        return this.httpClient.post<ApiRes>(url, data);
    }

    public postWithoutAuth<T>(url: string, data: T): Observable<ApiRes> {
        return this.httpClient.post<ApiRes>(url, data, { headers: this.skipAuthHeaders });
    }
}
