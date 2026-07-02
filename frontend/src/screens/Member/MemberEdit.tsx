import React, { useState, useEffect } from 'react';
import { useMutation } from '@tanstack/react-query';
import { TkButton, TkDatepicker, TkInput, TkSelect } from '@takeoff-ui/react';
import { dataProvider } from '../../dataProvider';
import { overlayStyle, modalStyle } from '../../styles';
import { LookupSelect } from '../../shared/LookupSelect';

type MemberRecord = {
  id: string | number;
  firstName: string;
  lastName: string;
  email?: string;
  phone?: string;
  birthDate?: string;
  membershipStartDate: string;
  membershipEndDate?: string;
  membershipStatus: string;
  totalSessions: number;
  usedSessions: number;
  notes?: string;
};

interface MemberEditProps {
  record: MemberRecord | null;
  onClose: () => void;
  onSuccess: () => void;
}

export const MemberEdit: React.FC<MemberEditProps> = ({ record, onClose, onSuccess }) => {
  const [form, setForm] = useState<Partial<MemberRecord>>(record ?? {});
  const setField = (name: string, value: unknown) => setForm((p) => ({ ...p, [name]: value }));

  useEffect(() => {
    if (record) setForm({ ...record });
  }, [record]);

  const mutation = useMutation({
    mutationFn: (values: Partial<MemberRecord>) =>
      dataProvider.update('Member', record!.id, values),
    onSuccess: (_data, values) => { onSuccess(); onClose(); },
    onError: (err: Error) => { window.dispatchEvent(new CustomEvent('app-toast', { detail: { type: 'error', message: err.message } })); },
  });

  if (!record) return null;

  return (
    <div style={overlayStyle} onClick={onClose}>
      <div style={modalStyle} onClick={(e) => e.stopPropagation()}>
        <div style={{ padding: '20px 28px 0', flexShrink: 0, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <h2 style={{ margin: 0, fontSize: 18, fontWeight: 700 }}>Edit Member</h2>
          <button onClick={onClose} style={{ background: 'none', border: 'none', fontSize: 20, cursor: 'pointer', color: '#666', padding: '4px 8px', borderRadius: 4 }} onMouseOver={(e) => (e.currentTarget.style.color = '#333')} onMouseOut={(e) => (e.currentTarget.style.color = '#666')}>✕</button>
        </div>
        <form onSubmit={(e) => { e.preventDefault(); mutation.mutate(form); }} style={{ display: 'flex', flexDirection: 'column', flex: 1, overflow: 'hidden' }}>
          <div style={{ flex: 1, overflowY: 'auto', padding: '20px 28px' }}>
            <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px' }}>
                <div>
                  <TkInput mode="text" label="First Name *" value={String(form.firstName ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('firstName', v))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="text" label="Last Name *" value={String(form.lastName ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('lastName', v))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="text" label="Email" value={String(form.email ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('email', v))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="text" label="Phone" value={String(form.phone ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('phone', v))(e.detail)} />
                </div>
                <div>
                  <TkDatepicker label="Birth Date" value={String(form.birthDate ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('birthDate', v))(e.detail)} />
                </div>
                <div>
                  <TkDatepicker label="Membership Start Date *" value={String(form.membershipStartDate ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('membershipStartDate', v))(e.detail)} />
                </div>
                <div>
                  <TkDatepicker label="Membership End Date" value={String(form.membershipEndDate ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('membershipEndDate', v))(e.detail)} />
                </div>
                <div>
                  <LookupSelect label="Membership Status *" value={String(form.membershipStatus ?? '')} onChange={(v) => setField('membershipStatus', v ? Number(v) : null)} searchable={false} options={[{ label: 'Active', value: '0' }, { label: 'Passive', value: '1' }, { label: 'Frozen', value: '2' }]} />
                </div>
                <div>
                  <TkInput mode="number" label="Total Sessions *" value={String(form.totalSessions ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('totalSessions', Number(v)))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="number" label="Used Sessions *" value={String(form.usedSessions ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('usedSessions', Number(v)))(e.detail)} />
                </div>
                <div>
                  <TkInput mode="text" label="Notes" value={String(form.notes ?? '')} onTkChange={(e: CustomEvent) => ((v) => setField('notes', v))(e.detail)} />
                </div>
            </div>
          </div>
          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, padding: '16px 28px', borderTop: '1px solid #e8e8e8', flexShrink: 0, background: '#fff' }}>
            <TkButton label="Cancel" variant="secondary" onTkClick={onClose} />
            <TkButton label={mutation.isPending ? 'Saving…' : 'Save Changes'} variant="primary" mode="submit" disabled={mutation.isPending} />
          </div>
        </form>
      </div>
    </div>
  );
};
