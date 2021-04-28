import { IQuizDTO } from "../IQuizDTO";
import { ITeamDTO } from "../ITeamDTO";
import { ITopicQuestionDTO } from "../ITopicQuestionDTO";

export interface ITeamAnswersCustomDTO{
    topicQuestionId: string;
    topicQuestion: ITopicQuestionDTO;
    teamId: string;
    team: ITeamDTO;
    answer: string;
    correct: boolean;
    points: number;

}