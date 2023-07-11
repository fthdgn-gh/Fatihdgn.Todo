import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class LocalStorageService {

    private _isAvailable: boolean | null = null;
    public get isAvailable(): boolean {
        if(this._isAvailable !== null) return this._isAvailable;

        try {
            const testKey = "__test_key__";
            localStorage.setItem(testKey, testKey);
            localStorage.removeItem(testKey);
            this._isAvailable = true;
        } catch (e) {
            this._isAvailable = false;
        }
        return this.isAvailable;
    }
  
    public get<T>(key: string): T | null {
        if (!this.isAvailable) return null;
        const value = localStorage.getItem(key);
        if(value == null) return null;
        return <T>JSON.parse(value);
    }

    public set<T>(key: string, value: T): void {
        if (!this.isAvailable) return;
        return localStorage.setItem(key, JSON.stringify(value));
    }

    public remove(key: string): void {
        if (!this.isAvailable) return;
        localStorage.removeItem(key);
    }

    public clear(){
        if (!this.isAvailable) return;
        localStorage.clear();
    }
}