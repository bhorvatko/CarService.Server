using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Model
{
    public class Warrant : Entity
    {
        public DateTime Deadline { get; protected set; }
        public WarrantType WarrantType { get; protected set; }
        public Step CurrentStep { get; protected set; }
        public bool IsUrgent { get; protected set; }
        public string Subject { get; protected set; }
        public int? TechnicianId { get; protected set; }
        public Technician? Technician { get; protected set; }
        public IEnumerable<Note> Notes { get; protected set; } = new List<Note>();

#nullable disable warnings
        private Warrant() { }
#nullable enable warnings

        public Warrant(DateTime deadline, WarrantType warrantType, bool isUrgent, string subject)
        {
            MapCommon(deadline, warrantType, isUrgent, subject);

            CurrentStep = warrantType.GetInitialStep();
            Notes = new List<Note>();
        }

        public void Update(DateTime deadline, WarrantType warrantType, bool isUrgent, string subject, int currentStepId, IEnumerable<string> notes)
        {
            MapCommon(deadline, warrantType, isUrgent, subject);

            CurrentStep = warrantType.Steps.First(s => s.Id == currentStepId);
            Notes = notes.Select(n => new Note(n)).ToList();
        }

        [MemberNotNull(nameof(WarrantType))]
        [MemberNotNull(nameof(Subject))]
        private void MapCommon(DateTime deadline, WarrantType warrantType, bool isUrgent, string subject)
        {
            Deadline = deadline;
            WarrantType = warrantType;
            IsUrgent = isUrgent;
            Subject = subject;
        }


        public Step AdvanceToNextStep()
        {
            Step? nextStep = CurrentStep.ForwardTransition?.TargetStep;

            if (nextStep == null) throw new InvalidOperationException($"Cannot advance warrant {Id} to next step. No next step found.");

            return SetCurrentStep(nextStep);
        }

        public Step RollbackToPreviousStep()
        {
            Step? previousStep = CurrentStep.BackTransition?.SourceStep;

            if (previousStep == null) throw new InvalidOperationException($"Cannot rollback warrant {Id} to previous step. No previous step found.");

            return SetCurrentStep(previousStep);
        }

        public void AssignToTechnician(Technician? technician)
        {
            Technician?.RemoveWarrant(this);

            TechnicianId = technician?.Id;
            Technician = technician;

            Technician?.AddWarrant(this);
        }

        private Step SetCurrentStep(Step step)
        {
            if (!WarrantType.Steps.Contains(step))
            {
                throw new InvalidOperationException("The warrant type's sequence does not contain the warrant's current step.");
            }

            CurrentStep = step;

            return step;
        }
    }
}
