import { IQuizDTO } from "../../domain/IQuizDTO";
import { IQuizTopicDTO } from "../../domain/IQuizTopicDTO";
import { ITeamDTO } from "../../domain/ITeamDTO";
import { ITopicQuestionDTO } from "../../domain/ITopicQuestionDTO";

interface IProps {
    quiz: IQuizDTO;
}


const PointsModal = (props: IProps) => (
    <div className="modal fade" id="pointsModal" tabIndex={-1} aria-labelledby="pointsModalLabel" aria-hidden="true">
        <div className="modal-dialog modal-xl">
            <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title" id="pointsModalLabel">Punktitabel</h5>
                    <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div className="modal-body">
                    <table className="table table-hover table-bordered border-color-black" style={{ "borderColor": "black" }}>
                        <thead style={{ "borderColor": "black" }}>
                            <tr className="table-info" style={{ "borderColor": "black" }}>
                                <th scope="col" style={{ "borderColor": "black" }}></th>
                                {props.quiz.quizTopics!.map((topic, i) => (
                                    <th scope="col" style={{ "borderColor": "black" }} colSpan={topic.topicQuestions!.length + 1}>{topic.topic}</th>
                                ))}
                                <th scope="col" style={{ "borderColor": "black" }}></th>
                            </tr>
                            <tr style={{ "borderColor": "black" }}>
                                <th scope="col" style={{ "borderColor": "black", "backgroundColor": "#f1d3ff" }}>Tiim</th>
                                {props.quiz.quizTopics!.map((topic, i) => (
                                    <>{topic.topicQuestions!.map((question, i) => (
                                        <th scope="col" style={{ "borderColor": "black", "backgroundColor": "#f1d3ff" }}>K{i + 1}</th>
                                    ))}
                                        <th scope="col" style={{ "borderColor": "black", "backgroundColor": "#ac92d4" }}></th>
                                    </>
                                )
                                )}
                                <th scope="col" style={{ "borderColor": "black", "backgroundColor": "#ac92d4" }}>Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            {props.quiz.teams!.map((team, i) => (
                                <tr>
                                    <th>{team.name}</th>
                                    {props.quiz.quizTopics!.map((topic, i) => (
                                        <>{topic.topicQuestions!.map((question, i) => (
                                            <th scope="col">{findPoints(question, team)}</th>
                                        ))}
                                            <th scope="col" style={{ "backgroundColor": "#f1d3ff" }}>{topicTotal(topic, team)}</th>
                                        </>
                                    )
                                    )}
                                    <th scope="col" style={{ "backgroundColor": "#ac92d4" }}>{findTotal(team)}</th>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                <div className="modal-footer">
                    <button type="button" className="btn btn-secondary" data-dismiss="modal">Sulge</button>
                </div>

            </div>
        </div>
    </div>
)

function findPoints(question: ITopicQuestionDTO, team: ITeamDTO) {
    var answer = team.teamAnswers!.find(a => a.topicQuestionId === question.id)
    if (answer) {
        return answer.points
    }
    return 0
}
function findTotal(team: ITeamDTO) {
    var sum = 0;
    team.teamAnswers?.forEach(a => {
        if (a.points) {
            sum += a.points
        }
    })
    return sum
}

function topicTotal(topic: IQuizTopicDTO, team: ITeamDTO) {
    var sum = 0;
    topic.topicQuestions?.forEach(q => {
        team.teamAnswers?.forEach(a => {
            if (a.topicQuestionId === q.id && a.points) {
                sum += a.points
            }
        })
    })
    return sum
}

export default PointsModal;