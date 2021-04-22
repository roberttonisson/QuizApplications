import { IBaseDomain } from '../base/IBaseDomain';
import { IAppUserDTO } from './identity/IAppUserDTO';
import { ITopicQuestionDTO } from './ITopicQuestionDTO';

export interface ISavedQuestionDTO extends IBaseDomain {
    topicQuestionId: string;
    topicquestion?: ITopicQuestionDTO;

    appUserId: string;
    appUser?: IAppUserDTO;

}