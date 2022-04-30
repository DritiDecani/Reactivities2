import React from "react";
import { Button, Item, ItemContent, ItemDescription, Label, Segment } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";

interface Props {
    activities: Activity[];
    selectActivity: (id: string) => void;
}
export default function ActivityList({ activities, selectActivity }: Props) {
    return (
        <Segment>
            <Item.Group divided>
                {activities.map(activity=>(
                    <Item key={activity.id}>
                        <ItemContent>
                            <Item.Header as='a'>{activity.title}</Item.Header>
                            <Item.Meta>{activity.date}</Item.Meta>
                            <ItemDescription>
                                <div>{activity.description}</div>
                                <div>{activity.city},{activity.venue}</div>
                            </ItemDescription>
                            <Item.Extra>
                                <Button onClick={() => selectActivity(activity.id)} floated="right" content='View' color="blue"/>
                                <Label basic content={activity.category}/>
                            </Item.Extra>
                        </ItemContent>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    );
}
