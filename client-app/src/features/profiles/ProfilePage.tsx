import React from "react";
import {
  Button,
  Divider,
  Grid,
  Header,
  Item,
  Reveal,
  Segment,
  Statistic,
} from "semantic-ui-react";
import ProfileHeader from "./ProfileHeader";

export default function ProfilePage() {
  return (
    
      <Grid>
        <Grid.Column width={16}>
            <ProfileHeader/>
        </Grid.Column>
      </Grid>
    
  );
}
