import { IBaseDomain } from "../../base/IBaseDomain";
export interface IAppUserDTO extends IBaseDomain {
    firstName: string;
    lastName: string;
    email: string;

    
}